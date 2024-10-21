using System;
using UnityEngine;

[RequireComponent(typeof(HeroMover))]
public class HeroRotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Enemy _firstEnemyToLookAt;
    private HeroMover _heroMover;
    private Enemy _targetEnemy;

    private bool _canRotate;
    private void Awake()
    {
        _heroMover = GetComponent<HeroMover>();
    }

    private void Update()
    {
        if (_canRotate)
        {
            LookAtTarget();
        }
    }

    private void OnEnable()
    {
        _heroMover.WaypointReached += OnWaypointReached;
    }

    private void Start()
    {
        _targetEnemy = _firstEnemyToLookAt;
    }

    private void LookAtTarget()
    {
        Quaternion originalRotation = transform.rotation;
        transform.LookAt(_targetEnemy.transform);
        Quaternion newRotation = transform.rotation;
        transform.rotation = originalRotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
    }

    private void OnDisable()
    {
        _heroMover.WaypointReached -= OnWaypointReached;
    }

    private void OnWaypointReached()
    {
        _canRotate = true;
        _targetEnemy.Died += OnEnemyDied;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _targetEnemy.Died -= OnEnemyDied;
        Debug.Log("OnEnemyDied");
        _targetEnemy = _heroMover.CurrentWaypoint.AliveEnemy;
        _targetEnemy.Died += OnEnemyDied;

    }
}
