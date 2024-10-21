using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(Enemy))] 
public class EnemyFollower : MonoBehaviour
{
    public event UnityAction<Vector3, float> DirectionToTargetUpdated;

    private Transform _target;


    private void Awake() => _target = FindObjectOfType<Player>().transform;

    private void Update() => SendDirection();

    private void SendDirection()
    {
        Vector3 direction = (_target.position - transform.position).normalized;

        float distance = (_target.position - transform.position).magnitude;

        DirectionToTargetUpdated?.Invoke(direction, distance);
    }
}
