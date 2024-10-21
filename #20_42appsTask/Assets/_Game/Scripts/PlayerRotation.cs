using UnityEngine;

namespace _Game.Scripts
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField] private float mouseSensitivity = 300f;
        [SerializeField] private Transform _camera;
        
        private float verticalRotation;
        private float horizontalRotation;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -45f, 45f);
            
            horizontalRotation += mouseX;
            
            _camera.localRotation = Quaternion.Euler(verticalRotation, 0, 0f);
            transform.localRotation = Quaternion.Euler(0, horizontalRotation, 0f);
        }
    }
}