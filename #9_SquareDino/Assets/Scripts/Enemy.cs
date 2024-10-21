using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _allRigidbodies;
    [SerializeField] private Animator _animator;
    
    public event Action<Enemy> Died;

    private void Awake()
    {
        foreach (Rigidbody t in _allRigidbodies)
        {
            t.isKinematic = true;
        }
    }


    private void MakePhysical()
    {
        foreach (Rigidbody t in _allRigidbodies)
        {
            _animator.enabled = false;
            t.isKinematic = false;
        }
    }

    public void Die()
    {
        MakePhysical();
        Died?.Invoke(this);
    }
}
