using UnityEngine;

public class PlayerPartsContanier : MonoBehaviour
{
    [SerializeField] private Vector2 _forceLimit;
    [SerializeField] private Vector2 _upwardsLimit;
    [SerializeField] private float _radius;
    [SerializeField] private float _explosionDeviation;

    private void OnEnable()
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody body in bodies)
            body.AddExplosionForce(Random.Range(_forceLimit.x, _forceLimit.y), GetRandomOrigin(), _radius, 
                                   Random.Range(_upwardsLimit.x, _upwardsLimit.y), ForceMode.VelocityChange);
    }

    private Vector3 GetRandomOrigin()
    {
        Vector3 origin = transform.position;
        origin.x += Random.Range(-_explosionDeviation, _explosionDeviation);
        origin.y += Random.Range(-_explosionDeviation, _explosionDeviation);
        origin.z += Random.Range(-_explosionDeviation, _explosionDeviation);

        return origin;
    }
}
