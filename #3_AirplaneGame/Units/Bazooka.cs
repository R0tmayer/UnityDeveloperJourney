using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : Unit
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _shotPeriod;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Transform _target;
    private Vector3 _normalizedDirection;
    private BulletCreator _bulletCreator;

    private void Awake()
    {
        _bulletCreator = GetComponentInChildren<BulletCreator>();
    }

    private void Update()
    {
        FollowTarget();
        _bulletCreator.Shoot(_bulletPrefab, _normalizedDirection, _bulletSpeed, _shotPeriod);
    }

    private void FollowTarget()
    {
        _normalizedDirection = (_target.position - transform.position).normalized;
        transform.up = _normalizedDirection;
    }
}