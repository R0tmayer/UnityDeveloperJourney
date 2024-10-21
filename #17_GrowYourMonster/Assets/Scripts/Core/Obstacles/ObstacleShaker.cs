using Core.Hero;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Obstacles
{
    public class ObstacleShaker : MonoBehaviour
    {
        [SerializeField] [Required] private ObstacleHealth _health;
        
        private bool _collided;
        private GameParameters _gameParameters;

        public void Construct(GameParameters gameParameters) => _gameParameters = gameParameters;

        public void StartShaking()
        {
            if (_collided)
                return;

            _collided = true;
            
            _health.StartDecreaseHealth();

            transform.DOShakePosition(_gameParameters.ShakeDuration, _gameParameters.ShakeStrength)
                .SetEase(_gameParameters.ShakeEase)
                .SetLoops(int.MaxValue)
                .SetId(this);
        }

        public void StopShaking()
        {
            _collided = false;
            _health.StopDecreaseHealth();
            DOTween.Kill(this);
        }
    }
}