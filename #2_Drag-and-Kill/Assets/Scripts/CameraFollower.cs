using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _moveOffsetY;
    [SerializeField] private float _moveOffsetZ;


    private void Update() => Follow();

    private void Follow()
    {
        Vector3 targetPosition = _player.position;
        targetPosition.y += _moveOffsetY;
        targetPosition.z += _moveOffsetZ;

        transform.position = Vector3.Lerp(transform.position, targetPosition, _moveSpeed * Time.deltaTime);
    }
}
