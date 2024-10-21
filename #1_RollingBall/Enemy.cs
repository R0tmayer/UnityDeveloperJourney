using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    private Rigidbody _rigidbody;
    private bool _canMove;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_canMove)
            _rigidbody.velocity = Vector3.forward * _speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Ground ground))
            _canMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DestroyerWall destroyer))
            Die();

        if (other.TryGetComponent(out Player player))
        {
            player.ApplyDamage(_damage);
            Die();
        }
    }

    private void Die()
    {
        _canMove = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
