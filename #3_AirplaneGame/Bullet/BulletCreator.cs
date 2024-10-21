using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCreator : MonoBehaviour
{
    [SerializeField] private AudioSource _shotSound;
    private float _timer;

    public void Shoot(Bullet bulletPrefab, Vector3 direction, float bulletSpeed,float shotPeriod)
    {
        if (_timer >= shotPeriod)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            _shotSound.pitch = Random.Range(0.8f, 1.2f);
            _shotSound.Play();
            bullet.Move(direction, bulletSpeed);
            _timer = 0;
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
    }
}
