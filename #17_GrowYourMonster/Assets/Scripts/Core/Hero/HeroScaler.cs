using System;
using System.Linq;
using Core.Obstacles;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Hero
{
    public class HeroScaler : MonoBehaviour
    {
        [SerializeField] [Required] private Transform _model;
        [SerializeField] [Required] private Attack _attacker;
        [SerializeField] [Required] private CameraZoom _cameraZoom ;
        
        private HeroScaleConfig _heroScaleConfig;
        private Vector3 _startScale;
        private int _currentIndex;

        public void Construct(HeroScaleConfig heroScaleConfig) => _heroScaleConfig = heroScaleConfig;

        private void Start() => _startScale = _model.localScale;

        public void IncreaseScale()
        {
            var scaleSheet = _heroScaleConfig.scaleSheet;
            
            if (_attacker.Rate < scaleSheet[0].rate)
                return;

            var additionalScale = Vector3.zero;
            
            FindCurrentIndex();

            if(additionalScale == Vector3.zero)
                return;

            var newScale = _startScale + additionalScale;

            _model.DOScale(newScale, 0.5f);

            if(_cameraZoom != null)
                _cameraZoom.ZoomOut(_currentIndex);

            void FindCurrentIndex()
            {
                while (_currentIndex < scaleSheet.Count)
                {
                    if (_attacker.Rate >= scaleSheet[_currentIndex].rate)
                    {
                        additionalScale = Vector3.one * scaleSheet[_currentIndex].additionalScale;
                        _currentIndex+=1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}