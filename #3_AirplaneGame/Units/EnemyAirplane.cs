using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class EnemyAirplane : Unit
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _target;
    [SerializeField] private float _shotPeriod;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _speed;
    private Vector3 _normalizedDirection;
    private BulletCreator _bulletCreator;
    private Rigidbody _rigidbody;
    private float _timer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _bulletCreator = GetComponentInChildren<BulletCreator>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        FollowTarget();

        if (_timer >= _shotPeriod)
        {
            _bulletCreator.Shoot(_bulletPrefab, _normalizedDirection, _bulletSpeed, _shotPeriod);
            _timer = 0;
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
    }

    private void FixedUpdate()
    {
        _rigidbody.AddRelativeForce(_speed, 0, 0);
    }

    private void FollowTarget()
    {
        _normalizedDirection = (_target.position - transform.position).normalized;
        transform.right = _normalizedDirection;
    }
}