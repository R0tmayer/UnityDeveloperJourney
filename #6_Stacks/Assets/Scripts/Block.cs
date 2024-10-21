using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Transform _myTransform;

    private void Awake() => _myTransform = GetComponent<Transform>();

    private void Update() => _myTransform.position -= new Vector3(Time.deltaTime * _moveSpeed, 0, 0);

    public void SetMoveSpeed(int value) => _moveSpeed = value;
}
