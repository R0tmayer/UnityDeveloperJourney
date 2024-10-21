using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Rigidbody _playerRigidbody;
    private List<Vector3> _velocityList = new List<Vector3>();

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            _velocityList.Add(transform.forward);
        }
    }

    private void FixedUpdate()
    {
        if(_playerRigidbody.velocity.magnitude > 0)
        {
            _velocityList.Add(_playerRigidbody.velocity);
            _velocityList.RemoveAt(0);
        }
    }

    private void Update()
    {
        Vector3 sumVector = Vector3.zero;

        for (int i = 0; i < _velocityList.Count; i++)
        {
            sumVector += _velocityList[i];
        }

        transform.position = _playerTransform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(sumVector), Time.deltaTime * 10f);
        
    }
}
