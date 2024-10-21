using System;
using System.Collections;
using UnityEngine;

namespace _Game.Scripts
{
    public class RagdollManager : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] _rigidbodies;

        private void OnCollisionEnter(Collision collision)
        {
            ActivateGravity(true);
        }

        public void WakeUp()
        {
            for (var i = 0; i < _rigidbodies.Length; i++)
            {
                _rigidbodies[i].sleepThreshold = 0;
                _rigidbodies[i].WakeUp();
            }
            
        }
        private void Awake()
        {
            MakeKinematic(true);
            ActivateGravity(false);
        }

        public void MakeKinematic(bool state)
        {
            for (var i = 0; i < _rigidbodies.Length; i++)
            {
                _rigidbodies[i].isKinematic = state;
            }
        }

        public void ActivateGravity(bool state)
        {
            for (var i = 0; i < _rigidbodies.Length; i++)
            {
                _rigidbodies[i].useGravity = state;
            }
        }

    }
}