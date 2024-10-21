using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Pillars;
using UnityEngine;

namespace Core.Character
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _duration = 2f;
        private Vector3 _basePosition;

        private void Awake()
        {
            _basePosition = transform.position;
        }

        public Tween GetMoveToPillarTween(Pillar pillar)
        {
            return GetMoveTween(pillar.transform.position);
        }

        public Tween GetMoveToBaseTween()
        {
            return GetMoveTween(_basePosition);
        }

        private Tween GetMoveTween(Vector3 destination)
        {
            var position = new Vector3(destination.x, transform.position.y, destination.z);
            return transform.DOMove(position, _duration).SetEase(Ease.Linear);
        }
        
    }
}
