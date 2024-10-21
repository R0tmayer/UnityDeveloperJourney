using Core.Animation;
using Core.Pickups;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Snake
{
    public class PickupHandler : MonoBehaviour
    {
        [SerializeField] [Required] private SnakeList _snakeList;
        [SerializeField] [Required] private SnakeScaleAnimation _collectAnimation;

        private void OnEnable()
        {
            _snakeList.LastPart.PickupCollected += OnPickupCollected;
        }

        private void OnDisable()
        {
            foreach (SnakePart part in _snakeList.Parts) part.PickupCollected -= OnPickupCollected;
        }

        private void OnPickupCollected(Pickup pickup)
        {
            pickup.AddSnakesComponents();
            pickup.Follower.StartFollowingObject(_snakeList.LastPart);

            _snakeList.AddNewPart(pickup);
            _snakeList.LastPart.PickupCollected += OnPickupCollected;

            _collectAnimation.Play();

            pickup.RemoveRigidbody();
            pickup.RemoveThisComponent();
        }
    }
}