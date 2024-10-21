using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Obstacles
{
    public class SwitcherToDividedPieces : MonoBehaviour
    {
        [SerializeField] [Required] private CollisionDetector _detector;
        [SerializeField] [Required] private ObstacleHealth _health;
        [SerializeField] [Required] private ObstacleShaker _shaker;
        [SerializeField] [Required] private ObstacleScaler _scaler;
        [SerializeField] [Required] private Collider _collider;
        [SerializeField] [Required] private GameObject _dividedParent;

        private List<Rigidbody> _childrenRigidbodies;

        private GameParameters _gameParameters;

        public void Construct(GameParameters gameParameters) => _gameParameters = gameParameters;

        private void Awake() => _childrenRigidbodies = _dividedParent.GetComponentsInChildren<Rigidbody>().ToList();

        public void Switch()
        {
            _shaker.StopShaking();
            _shaker.gameObject.SetActive(false);
            _collider.enabled = false;
            _dividedParent.SetActive(true);
            _scaler.ScaleToZeroAfterDelay();
            _detector.CurrentBlendShape.Decrease(_health.StartValue);
            AddForceToDividedPieces();
            StartCoroutine(DisableCollidersWithDelay());
        }

        private void AddForceToDividedPieces()
        {
            for (int i = 0; i < _childrenRigidbodies.Count; i++)
            {
                var direction = _detector.CurrentAttacker.transform.forward;
                _childrenRigidbodies[i].AddForce(direction * _gameParameters.ObstacleForce, ForceMode.Impulse);

                if (_health.StartValue == 1)
                {
                    _childrenRigidbodies[i].gameObject.layer = LayerMask.NameToLayer("IgnoreMonster");
                }
            }
        }

        private IEnumerator DisableCollidersWithDelay()
        {
            yield return new WaitForSeconds(0.85f);

            for (int i = 0; i < _childrenRigidbodies.Count; i++)
            {
                _childrenRigidbodies[i].gameObject.layer = LayerMask.NameToLayer("IgnoreMonster");
                yield return null;
            }
        }
    }
}