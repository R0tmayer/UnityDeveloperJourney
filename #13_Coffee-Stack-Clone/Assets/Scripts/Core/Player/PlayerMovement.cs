using Core.Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] [Required] private InputSwipeReceiver _input;
        [SerializeField] [Required] private Transform _roadTransform;
        [SerializeField] [Required] private Transform _bodyTransform;
        [SerializeField] private float _speed;

        private void Update()
        {
            Move();
            SwipePosition();
        }

        private void Move()
        {
            transform.Translate(Vector3.forward * _speed * Time.smoothDeltaTime);
        }

        private void SwipePosition()
        {
            Vector3 position = transform.position;
            var border = _roadTransform.localScale.x / 2f - _bodyTransform.localScale.x / 2f;

            var newPosition = new Vector3(
                position.x + _input.DeltaX,
                position.y,
                position.z);

            newPosition.x = Mathf.Clamp(newPosition.x, -border, border);

            transform.position = newPosition;
        }
    }
}