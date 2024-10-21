
using System;
using UnityEngine;

namespace _Game.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private int _horizontalSpeed = 5;
        [SerializeField] private int _verticalSpeed = 5;
        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            var movement = new Vector3(Input.GetAxisRaw("Horizontal") * _horizontalSpeed, 0,Input.GetAxisRaw("Vertical") * _verticalSpeed);
            movement = transform.TransformDirection(movement);
            _characterController.Move(movement * Time.deltaTime);
        }
    }
}