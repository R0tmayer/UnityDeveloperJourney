using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Door : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;
    private List<Room> _myRooms = new List<Room>();

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Room room))
        {
            _myRooms.Add(room);
        }
    }

    public void SetOpeningState(bool isOpen)
    {
        _boxCollider.enabled = false;
        float height = isOpen ? 3 : -3;

        transform.localPosition = new Vector3(transform.localPosition.x,
                                              transform.localPosition.y + height,
                                              transform.localPosition.z);
    }

    public List<Room> GetRooms()
    {
        return _myRooms;
    }
}
