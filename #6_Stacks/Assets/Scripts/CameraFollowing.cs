using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _camera;

    private Vector3 _offset;

    private void Start() => _offset = _camera.transform.position - _target.transform.position;

    private void Update() => _camera.transform.position = _target.transform.position + _offset;
}
