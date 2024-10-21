using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    private float _speed;

    public Transform Target { get; set; }

    private void Awake()
    {
        _speed = Random.Range(8, 12);
    }

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 normalizedDirection = (Target.position - transform.position).normalized;
        transform.forward = normalizedDirection * _speed;
        transform.position = Vector3.MoveTowards(transform.position, Target.position, Time.deltaTime * _speed);
    }

    public void Die()
    {
        Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        //звуки эффекты + очки 
    }
}
