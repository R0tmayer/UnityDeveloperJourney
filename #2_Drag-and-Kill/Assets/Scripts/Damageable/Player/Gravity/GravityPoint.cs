using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class GravityPoint : MonoBehaviour
{
    [SerializeField] private float _mass;
    [SerializeField] private float _gravityModifier;
    [SerializeField] private float _maxForce;
    
    [SerializeField] private Vector2 _affectedBodiesMassLimits;

    private readonly List<Rigidbody> _affectedRigidbodies = new List<Rigidbody>();


    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            Deviation deviation = DeviationChecker.GetDeviation(_affectedBodiesMassLimits, other.attachedRigidbody.mass);
            if (deviation == Deviation.InLimits)
            {
                _affectedRigidbodies.Add(other.attachedRigidbody);
            }
        }  
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody != null)
            _affectedRigidbodies.Remove(other.attachedRigidbody);
    }

    private void Update()
    {
        _affectedRigidbodies.RemoveAll(x => x == null || x.isKinematic || !x.gameObject.activeSelf || !x.gameObject.activeInHierarchy);
        foreach (Rigidbody rigidbody in _affectedRigidbodies)
        {
            if (rigidbody != null)
            {
                Vector3 direction = (transform.position - rigidbody.position).normalized;

                float force = GetForce(rigidbody);
                rigidbody.AddForce(direction * force);
            }
        }
    }

    private float GetForce(Rigidbody rigidbody)
    {
        float distance = (transform.position - rigidbody.position).magnitude;
        float force = _gravityModifier * rigidbody.mass * _mass / Mathf.Pow(distance, 2);
        if (force > _maxForce)
            force = _maxForce;

        return force;
    }
}
