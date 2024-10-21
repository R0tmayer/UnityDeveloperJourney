using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private AudioSource _hitSound;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ground ground))
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(_hitEffect, transform.position, Quaternion.identity);
        _hitSound.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        Destroy(gameObject, _hitSound.clip.length);
    }

    public void Move(Vector3 direction, float bulletSpeed)
    {
        _rigidbody.velocity = direction * bulletSpeed;
    }
}
