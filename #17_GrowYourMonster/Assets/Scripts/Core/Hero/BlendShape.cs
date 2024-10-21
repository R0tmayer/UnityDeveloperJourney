using System;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Hero
{
    public class BlendShape : MonoBehaviour
    {
        [SerializeField] [Required] private SkinnedMeshRenderer _skinnedMeshRenderer;
        private float _value = 100f;

        private GameParameters _gameParameters;

        public void Construct(GameParameters gameParameters) => _gameParameters = gameParameters;

        private void LateUpdate() => _skinnedMeshRenderer.SetBlendShapeWeight(0, _value);

        public void Decrease(int value)
        {
            var newValue = _value - value * _gameParameters.BlendShapeRate;
            
            if (newValue < 0)
            {
                newValue = 0;
            }
            
            DOTween.To(() => _value, x => _value = x, newValue, 0.5f);
            
        }
    } 
}