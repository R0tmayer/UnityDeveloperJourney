using System;
using System.Linq;
using UnityEngine;

namespace _Game.Scripts
{
    public class Spear : MonoBehaviour
    {
        [SerializeField] private Joint[] _joints;

        private bool _connected;

        private void OnEnable()
        {
            for (var i = 0; i < _joints.Length; i++)
            {
                _joints[i].GetComponent<JointCollision>().Collided += ConnectBone;
            }
        }

        private void OnDisable()
        {
            for (var i = 0; i < _joints.Length; i++)
            {
                _joints[i].GetComponent<JointCollision>().Collided -= ConnectBone;
            }
        }

        private void ConnectBone(Collider other, Joint joint)
        {
            joint.connectedBody = other.attachedRigidbody;
            other.GetComponentInParent<RagdollManager>().ActivateGravity(true);
        }

        public void DisconnectBones()
        {
            for (var i = 0; i < _joints.Length; i++)
            {
                _joints[i].connectedBody = null;
                _joints[i].GetComponent<Collider>().enabled = true;
            }
        }
    }
}