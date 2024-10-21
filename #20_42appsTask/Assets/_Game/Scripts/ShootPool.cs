using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace _Game.Scripts
{
    public class ShootPool : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _container;
        [SerializeField] private int _spawnCount;
        [SerializeField] private int _maxCount;

        private ObjectPool<Bullet> _pool;
        private Transform _cameraTransform;

        private void Awake()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void Start()
        {
            _pool = new ObjectPool<Bullet>(() => Instantiate(_bulletPrefab, _container),
                ActionOnGet,
                ActionOnRelease,
                ActionOnDestroy,
                false,
                _spawnCount, _maxCount);

            CreatePool();
        }

        private void ActionOnGet(Bullet bullet)
        {
            var bulletTransform = bullet.transform;
            
            bulletTransform.position = _cameraTransform.position + _cameraTransform.transform.forward;
            bulletTransform.forward = _cameraTransform.forward;
            
            bullet.Init(DisableBullet);
            bullet.Disabled += DisableBullet;
            bullet.gameObject.SetActive(true);
            bullet.Move();
        }

        private void DisableBullet(Bullet bullet)
        {
            _pool.Release(bullet);
        }

        private void ActionOnRelease(Bullet bullet)
        {
            bullet.Disabled -= DisableBullet;
            bullet.gameObject.SetActive(false);
        }

        private void ActionOnDestroy(Bullet bullet)
        {
            Destroy(bullet.gameObject);
        }

        private void CreatePool()
        {
            var bullets = new Bullet[_spawnCount];

            for (int i = 0; i < _spawnCount; i++)
            {
                bullets[i] = _pool.Get();
            }

            for (int i = 0; i < _spawnCount; i++)
            {
                _pool.Release(bullets[i]);
            }
        }

        public void GetBullet()
        {
            _pool.Get();
        }
    }
}