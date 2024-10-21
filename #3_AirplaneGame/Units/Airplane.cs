using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Airplane : Unit
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shotPeriod;
    [SerializeField] private float _speed;
    private BulletCreator _bulletCreator;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _bulletCreator = GetComponentInChildren<BulletCreator>();
    }

    private void Update()
    {
        FollowTarget();
    }

    private void FixedUpdate()
    {
        _rigidbody.AddRelativeForce(_speed, 0, 0);

        if (Input.GetMouseButton(0))
        {
            _bulletCreator.Shoot(_bulletPrefab, transform.right, _bulletSpeed, _shotPeriod);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Ground ground))
        {
            Die();
        }

        else if (collision.collider.TryGetComponent(out Unit unit))
        {
            unit.Die();
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Mine mine))
        {
            mine.Die();
            TakeDamage(5);
        }
        
        else if (other.TryGetComponent(out Rocket rocket))
        {
            rocket.Die();
            TakeDamage(2);
        }

        else if (other.TryGetComponent(out Coin coin))
        {
            coin.Collect();
        }
    }

    private void FollowTarget()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 airplanePosition = Camera.main.WorldToScreenPoint(transform.position);
        mousePosition.z = 0;
        airplanePosition.z = 0;
        Vector3 normalizedDiretcion = (mousePosition - airplanePosition).normalized;
        transform.right = normalizedDiretcion;
    }
}