using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerTouchContainer))]
public class PlayerPusher : MonoBehaviour
{
    [SerializeField] private GroundChecker _groundChecker;

    [SerializeField] private short _maxPushes;
    [SerializeField] private float _pushForceModifier;

    [SerializeField] private Vector2 _pushForceLimits;
    [SerializeField] private Vector2 _pushForceUpwardsLimits;

    private PlayerTouchContainer _touchContainer;
    private Rigidbody _rigidbody;

    private int _freePushes;


    private void OnEnable()
    {
        _groundChecker.GroundEntered += UpdateJumps;
        _touchContainer.PositionsDeleted += TryPush;
    }

    private void OnDisable()
    {
        _groundChecker.GroundEntered -= UpdateJumps;
        _touchContainer.PositionsDeleted -= TryPush;
    }

    private void Awake()
    {
        _touchContainer = GetComponent<PlayerTouchContainer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start() => _freePushes = _maxPushes;

    private void UpdateJumps() => _freePushes = _maxPushes;

    private void TryPush(Vector2 startPosition, Vector2 currentPosition)
    {
        if (_freePushes > 0)
        {
            float pushForce = GetForce(startPosition, currentPosition) * _pushForceModifier;
            pushForce = Mathf.Clamp(pushForce, _pushForceLimits.x, _pushForceLimits.y);

            Vector3 force = transform.forward * pushForce;
            force.y += GetPushForceUpwards(pushForce);

            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(force, ForceMode.VelocityChange);

            _freePushes--;
        }
    }

    private float GetPushForceUpwards(float pushForce)
    {
        float pushPercent = (pushForce * 100f) / _pushForceLimits.y;

        return (pushPercent * _pushForceUpwardsLimits.y) / 100f;
    }

    private float GetForce(Vector3 startPosition, Vector3 currentPosition)
    {
        startPosition = Camera.main.ScreenToViewportPoint(startPosition);
        currentPosition = Camera.main.ScreenToViewportPoint(currentPosition);

        return (currentPosition - startPosition).magnitude;
    }
}
