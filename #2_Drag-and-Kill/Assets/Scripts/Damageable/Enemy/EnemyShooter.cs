using UnityEngine;

[RequireComponent(typeof(Gun))]
[RequireComponent(typeof(EnemyFollower))]
public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private float _shotPeriod;
    [SerializeField] private Vector2 _distanceToTargetLimits;

    private Gun _gun;
    private EnemyFollower _enemy;

    private float _timer = 0f;


    private void OnEnable() => _enemy.DirectionToTargetUpdated += TryShoot;

    private void OnDisable() => _enemy.DirectionToTargetUpdated -= TryShoot;

    private void Awake()
    {
        _gun = GetComponent<Gun>();
        _enemy = GetComponent<EnemyFollower>();
    }

    private void Update() => _timer += Time.deltaTime;

    private void TryShoot(Vector3 direction, float distance)
    {
        if (_timer >= _shotPeriod)
        {
            if (DeviationChecker.GetDeviation(_distanceToTargetLimits, distance) == Deviation.InLimits)
            {
                _gun.Shoot(direction);
                _timer = 0f;
            }
        }
    }
}
