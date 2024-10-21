using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private AudioSource _explosionSound;
    [SerializeField] private int _health;

    public void TakeDamage()
    {
        _health--;

        if (_health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        _explosionSound.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        Destroy(gameObject, _explosionSound.clip.length);
    }
}
