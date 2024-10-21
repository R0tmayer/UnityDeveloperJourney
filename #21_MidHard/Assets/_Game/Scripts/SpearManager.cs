using Dreamteck.Splines;
using UnityEngine;

namespace _Game.Scripts
{
    public class SpearManager : MonoBehaviour
    {
        [SerializeField] private SplineComputer _splineComputer;
        [SerializeField] private SplineFollower _splineFollower;
        [SerializeField] private Transform _wall;
        [SerializeField] private Spear _spear;

        private SplinePoint _middlePoint;
        private bool _isFollowing;
        private bool _holdingDown;

        private void Start()
        {
            _middlePoint = _splineComputer.GetPoint(1);
        }

        private void Update()
        {
            Swipe();
        }

        private void SetParent(double value)
        {
            transform.SetParent(_wall);
        }

        private Vector3 GetNewMiddlePointPosition()
        {
            return new Vector3(transform.position.x, _splineComputer.GetPoint(0).position.y, _middlePoint.position.z);
        }

        private void Swipe()
        {
            if (Input.anyKey)
            {
                _spear.DisconnectBones();
                _splineFollower.follow = false;
                _splineFollower.SetDistance(0);
                _isFollowing = false;
                _holdingDown = true;
            }

            if (!Input.anyKey && _holdingDown)
            {
                _splineFollower.follow = true;
                _splineFollower.enabled = true;
                _splineFollower.onEndReached += SetParent;
                _isFollowing = true;
                _holdingDown = false;
            }

            if (_isFollowing || !_holdingDown)
                return;

            Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            transform.position = new Vector3(worldPos.x, transform.position.y,
                transform.position.z);
            _splineComputer.SetPointPosition(0, transform.position);
            _splineComputer.SetPointPosition(1, GetNewMiddlePointPosition());
        }
    }
}