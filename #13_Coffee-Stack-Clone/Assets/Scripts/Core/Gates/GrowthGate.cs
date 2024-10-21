using System;
using Core.Snake;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Gates
{
    public class GrowthGate : MonoBehaviour
    {
        [SerializeField] private float _scale;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BodiesSwitcher bodiesSwitcher))
            {
                bodiesSwitcher.SwitchNextBody();
                if (bodiesSwitcher.TryGetComponent(out SnakePart part))
                    part.ScaleAndRollback(_scale, _duration, _ease);
            }
        }
    }
}