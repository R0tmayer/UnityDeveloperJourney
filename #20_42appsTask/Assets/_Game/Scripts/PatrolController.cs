using System;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts
{
    public class PatrolController : MonoBehaviour
    {
        [SerializeField] private Transform _groundTransform;

        private void Start()
        {
            InitPatrolPath();
        }

        private void InitPatrolPath()
        {
            var leftBoarder = _groundTransform.position.x - _groundTransform.localScale.x / 2f;
            var rightBoarder = _groundTransform.position.x + _groundTransform.localScale.x / 2f;

            var position = transform.position;

            var firstPoint = new Vector3(leftBoarder, position.y, position.z);
            var secondPoint = new Vector3(rightBoarder, position.y, position.z);

            var path = new[] {firstPoint, secondPoint};

            transform.DOPath(path, 2)
                .SetOptions(true)
                .SetEase(Ease.Linear)
                .SetLoops(int.MaxValue);
        }
    }
}