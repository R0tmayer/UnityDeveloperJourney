using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Vector2 _activeTimeLimits;
    [SerializeField] private LayerMask _mask;

    private Rigidbody _rigidbody;

    private float _timer = 0f;


    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _activeTimeLimits.y)
            Die();
    }

    public void Move(Vector3 direction, float bulletSpeed) => _rigidbody.velocity = direction * bulletSpeed;

    private void Die() => Destroy(gameObject);

    private void OnTriggerEnter(Collider other)
    {
        if (_timer >= _activeTimeLimits.x)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeHit();

                Die();
            }
            else if ((_mask.value & (1 << other.gameObject.layer)) != 0)
            {
                Die();
            }
        }
    }
}
