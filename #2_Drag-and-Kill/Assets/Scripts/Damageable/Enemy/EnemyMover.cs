using UnityEngine;

[RequireComponent(typeof(EnemyFollower))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _maxVelocty;
    [SerializeField] private Vector2 _distanceToTargetLimits;

    private EnemyFollower _enemy;
    private Rigidbody _rigidbody;


    private void OnEnable() => _enemy.DirectionToTargetUpdated += TryMoveTo;

    private void OnDisable() => _enemy.DirectionToTargetUpdated -= TryMoveTo;

    private void Awake()
    {
        _enemy = GetComponent<EnemyFollower>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void TryMoveTo(Vector3 direction, float distance)
    {
        switch(DeviationChecker.GetDeviation(_distanceToTargetLimits, distance))
        {
            case Deviation.InLimits: _rigidbody.velocity = Vector3.zero; break;
            case Deviation.Less: MoveTo(direction); break;
            case Deviation.More: MoveTo(-direction); break;
        }
    }

    private void MoveTo(Vector3 direction)
    {
        direction.y = 0f;

        Vector3 force = direction * _moveSpeed * Time.deltaTime;
        _rigidbody.AddForce(force, ForceMode.VelocityChange);

        _rigidbody.velocity = GetLimitedVector3(_rigidbody.velocity);
        /*Vector3 targetVelocity = direction * _moveSpeed * Time.deltaTime;
        targetVelocity.y = _rigidbody.velocity.y;

        _rigidbody.velocity = targetVelocity;*/
    }

    private Vector3 GetLimitedVector3(Vector3 vector)
    {
        vector.x = GetLimitedValue(vector.x);
        vector.y = GetLimitedValue(vector.y);
        vector.z = GetLimitedValue(vector.z);

        return vector;
    }

    private float GetLimitedValue(float value)
    {
        if (value >= 0)
        {
            if (value > _maxVelocty)
            {
                value = _maxVelocty;
            }
        }
        else
        {
            if (value < -_maxVelocty)
            {
                value = -_maxVelocty;
            }
        }

        return value;
    }
}
