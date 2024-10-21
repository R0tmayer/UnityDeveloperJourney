using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _rotationSensivity;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    private float _multiplySpeed;
    private Rigidbody _rigidbody;
    private float _rotationX;
    private float _rotationY;
    private bool _isGround;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Application.targetFrameRate = 60;
        _multiplySpeed = _speed * 3;
    }

    private void Move(float speed)
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 speedVector = transform.TransformVector(inputVector * speed);
        _rigidbody.velocity = new Vector3(speedVector.x, _rigidbody.velocity.y, speedVector.z);
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            UpdatePlayerGaze();
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move(_speed);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Move(_multiplySpeed);
        }
    }

    private void UpdatePlayerGaze()
    {
        RotateCameraVertical();
        RotatePlayerHorizontal();
    }

    private void RotateCameraVertical()
    {
        _rotationX -= Input.GetAxis("Mouse Y") * _rotationSensivity;
        _rotationX = Mathf.Clamp(_rotationX, -45f, 45f);
        _cameraTransform.transform.localEulerAngles = new Vector3(_rotationX, 0f, 0f);
    }
    private void RotatePlayerHorizontal()
    {
        _rotationY = Input.GetAxis("Mouse X") * _rotationSensivity;
        transform.localEulerAngles += new Vector3(0, _rotationY, 0f);
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionStay(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;
        float dot = Vector3.Dot(normal, Vector3.up);

        if(dot > 0.7f)
        {
            _isGround = true;
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGround = false;
    }
}

