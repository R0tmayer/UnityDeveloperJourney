using Assets.Scripts.Configs;
using Assets.Scripts.Enemies;
using Assets.Scripts.VirtualJoystick;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Shooting
{
    public class ShootingController : MonoBehaviour
    {
        [SerializeField] [Required] private Joystick _joystick;
        [SerializeField] [Required] private Bullet _bulletPrefab;
        [SerializeField] [Required] private Transform _bulletCreatorTransform;
        [SerializeField] [Required] private EnemySearcher _enemySearcher;
        [SerializeField] [Required] private LayerMask _enemyLayerMask;
        [SerializeField] [Required] [AssetSelector] private CharacterSettings _settings;

        private readonly List<Bullet> _bulletPool = new List<Bullet>();
        private IEnumerator _shootingCoroutine;
        private WaitForSeconds _waitForSeconds;
        private Enemy _target;
        private Vector3 _directionToTarget;

        private void CreatePool()
        {
            for (int i = 0; i < _settings.BulletPoolCapacity; i++)
            {
                var bullet = Instantiate(_bulletPrefab);
                bullet.gameObject.SetActive(false);
                _bulletPool.Add(bullet);
            }
        }

        private void Start()
        {
            CreatePool();
            _waitForSeconds = new WaitForSeconds(_settings.ShootingPeriod);
            _shootingCoroutine = Shoot();
        }

        private void OnEnable()
        {
            _joystick.JoystickClickUp += OnJoystickClickUp;
            _joystick.JoystickClickDown += OnJoystickClickDown;
        }

        private void OnDisable()
        {
            _joystick.JoystickClickUp -= OnJoystickClickUp;
            _joystick.JoystickClickDown -= OnJoystickClickDown;
        }

        private void OnJoystickClickDown() => StopShooting();
        private void OnJoystickClickUp() => TryRaycastEnemy();
        private void OnTargetDied() => StopShooting();

        private void StopShooting()
        {
            if (_shootingCoroutine != null)
            {
                StopCoroutine(_shootingCoroutine);
                _shootingCoroutine = null;
            }
        }

        private void StartShooting()
        {
            if (_shootingCoroutine == null)
            {
                _shootingCoroutine = Shoot();
                StartCoroutine(_shootingCoroutine);
            }
        }

        private bool TryRaycastEnemy()
        {
            _directionToTarget = (_enemySearcher.ClosestEnemy.transform.position - _bulletCreatorTransform.position).normalized;
            
            Debug.DrawLine(_bulletCreatorTransform.position, _enemySearcher.ClosestEnemy.transform.position, Color.red, 1000);
            var ray = new Ray(_bulletCreatorTransform.position, _directionToTarget);

            if (Physics.Raycast(ray, out RaycastHit hit, int.MaxValue, _enemyLayerMask))
            {
                StartShooting();
                hit.transform.TryGetComponent(out Enemy enemy);
                _target = enemy;
                _target.Died += OnTargetDied;
                return true;
            }

            if (_target != null)
            {
                _target.Died -= OnTargetDied;
                _target = null;
            }

            return false;
        }


        private IEnumerator Shoot()
        {
            var cantShoot = !TryRaycastEnemy();

            if (cantShoot)
                yield break;

            while (true)
            {
                var bullet = GetFirstDeactivatedBullet();

                if (bullet == null)
                {
                    Debug.LogError("Bullet from pool is NULL");
                    yield break;
                }

                bullet.transform.position = _bulletCreatorTransform.position;
                bullet.MoveInDirection(_directionToTarget);

                yield return _waitForSeconds;
            }
        }

        private Bullet GetFirstDeactivatedBullet()
        {
            foreach (var bullet in _bulletPool)
            {
                if (bullet.gameObject.activeSelf) continue;

                bullet.gameObject.SetActive(true);
                return bullet;
            }

            return null;
        }
    }
}
