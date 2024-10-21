using System;
using Core.Input;
using UnityEngine;

namespace Core.Hero
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private InputJoystickReceiver _input;
        private Rigidbody _rigidbody;

        public void Construct(InputJoystickReceiver input) => _input = input;

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        private void Update()
        {
            var direction = new Vector3(_input.Direction.x, 0, _input.Direction.y);
            _rigidbody.velocity = direction * _speed;
            
            if (_input.DirectionIsZero)
                return;

            LookAt(direction);
        }

        private void LookAt(Vector3 direction)
        {
            var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, Time.deltaTime);
        }
    }
}