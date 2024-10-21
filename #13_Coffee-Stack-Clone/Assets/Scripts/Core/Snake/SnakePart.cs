using System;
using Core.Gates;
using Core.Pickups;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Snake
{
    public class SnakePart : MonoBehaviour
    {
        private Vector3 _startScale;

        private void Awake()
        {
            _startScale = transform.localScale;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Pickup pickup)) PickupCollected?.Invoke(pickup);
        }

        public Sequence ScaleAndRollback(float newScale, float duration, Ease ease)
        {
            return DOTween.Sequence()
                .Append(transform.DOScale(newScale, duration / 2f))
                .Append(transform.DOScale(_startScale, duration / 2f))
                .SetEase(ease);
        }

        public event Action<Pickup> PickupCollected;
    }
}