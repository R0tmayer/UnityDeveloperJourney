using DG.Tweening;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Game Parameters", menuName = "ScriptableObjects/GameParameters", order = 1)]
    public class GameParameters : ScriptableObject
    {
        [SerializeField] private Ease _shakeEase;
        [SerializeField] [Range(0.1f, 0.3f)] private float _shakeDuration;
        [SerializeField] [Range(0.2f, 0.5f)] private float _shakeStrength;
        [SerializeField] [Range(1, 2)] private float _obstacleScaleDuration;
        [SerializeField] [Range(1, 2)] private float _obstacleScaleDelay;
        [SerializeField] [Range(5, 10)] private float _obstacleForce;
        [SerializeField] [Range(0.1f, 1f)] private float _heroAttackModificator;
        [SerializeField] [Range(0.1f, 0.2f)] private float _blendShapeRate;

        public Ease ShakeEase => _shakeEase;
        public float ShakeDuration => _shakeDuration;
        public float ShakeStrength => _shakeStrength;
        public float ObstacleScaleDelay => _obstacleScaleDelay;
        public float ObstacleForce => _obstacleForce;
        public float ObstacleScaleDuration => _obstacleScaleDuration;
        public float HeroAttackModificator => _heroAttackModificator;
        public float BlendShapeRate => _blendShapeRate;
    }
}