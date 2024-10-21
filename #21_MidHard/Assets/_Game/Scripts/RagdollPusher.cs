using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts
{
    public class RagdollPusher : MonoBehaviour
    {
        [SerializeField] private Rigidbody _pelvisRB;
        [SerializeField] private float _force;
        [SerializeField] private ForceMode _forceMode;
        [SerializeField] private RagdollManager _ragdollManager;
        [SerializeField] private float _yDirection;

        private void OnEnable()
        {
            _ragdollManager.MakeKinematic(false);
            _ragdollManager.WakeUp();
            _ragdollManager.ActivateGravity(true);
            
        }

        private void Start()
        {
            var x = transform.position.x > 0 ? Random.Range(-0.5f, -0.25f) : Random.Range(0.25f, 0.5f);
            var forceDirection = new Vector3(x, _yDirection, 0);
            _pelvisRB.AddForce(forceDirection * _force, _forceMode);
        }
    }
}