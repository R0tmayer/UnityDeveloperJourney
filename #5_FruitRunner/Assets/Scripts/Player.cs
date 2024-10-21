using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speedValue;
    [SerializeField] private float _swipeLerpRate;
    private float _offsetX;

    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private float _jumpPeriod;
    [SerializeField] private float _jumpHeight;
    private float _jumpTimer;
    private bool _isGrounded = true;

    [SerializeField] private LevelSceneController _levelSceneController;
    [SerializeField] private AnimationController _animatorController;

    public event Action<Fruit> FruitCollected;
    public event Action Died;
    public event Action Grounded;
    public event Action Jumped;

    private void Awake()
    {
        _jumpTimer = 20;
    }

    private void Update()
    {
        transform.position += new Vector3(0, 0, Time.deltaTime * _speedValue);
        SwipeXPosition();
        Jump();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Fruit fruit))
        {
            FruitCollected?.Invoke(fruit);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            Died?.Invoke();
        }

        if (collision.gameObject.TryGetComponent(out Ground ground))
        {
            _isGrounded = true;
            Grounded?.Invoke();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }

    private void SwipeXPosition()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _offsetX--;

            if (_offsetX <= -1)
            {
                _offsetX = -1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _offsetX++;

            if (_offsetX >= 1)
            {
                _offsetX = 1;
            }
        }

        float _offsetWidth = 2;
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(_offsetX * _offsetWidth, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * _swipeLerpRate);
    }

    private void Jump()
    {
        _jumpTimer += Time.deltaTime / _jumpPeriod;
        float height = _jumpCurve.Evaluate(_jumpTimer) * _jumpHeight;

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded == true)
        {
            Jumped?.Invoke();
            _jumpTimer = 0;
        }

        transform.position = new Vector3(transform.position.x, height, transform.position.z);
    }
}
