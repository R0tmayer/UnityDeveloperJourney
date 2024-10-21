using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform _airplane;
    [SerializeField] private float _offsetZ;
    private Vector3 _position;

    private void Update()
    {
        _position = new Vector3(_airplane.position.x, _airplane.position.y, _airplane.position.z - _offsetZ);
        transform.position = _position;
    }
}
