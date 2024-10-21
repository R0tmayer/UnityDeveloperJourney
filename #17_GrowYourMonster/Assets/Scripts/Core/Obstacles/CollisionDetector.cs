using Core.Hero;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Obstacles
{
    public class CollisionDetector : MonoBehaviour
    {
        [SerializeField] [Required] private ObstacleShaker _shaker;
        public BlendShape CurrentBlendShape { get; private set; }
        public AnimatorController CurrentAnimator { get; private set; }
        public Attack CurrentAttacker { get; private set; }
        public HeroScaler HeroScaler { get; private set; }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out BlendShape blendShape))
            {
                CurrentBlendShape = blendShape;
                CurrentAttacker = blendShape.GetComponent<Attack>();
                HeroScaler = blendShape.GetComponent<HeroScaler>();
                CurrentAnimator = blendShape.GetComponent<AnimatorController>();
                CurrentAnimator.SetAttackState();
                _shaker.StartShaking();
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.TryGetComponent(out BlendShape _))
            {
                CurrentAnimator.ResetAttackState();
                CurrentBlendShape = null;
                CurrentAttacker = null;
                HeroScaler = null;
                _shaker.StopShaking();
            }
        }
        
    }
}