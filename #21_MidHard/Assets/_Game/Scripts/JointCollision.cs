using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class JointCollision : MonoBehaviour
    {
        public event Action<Collider, Joint> Collided;

        private bool _connected;

        private void OnTriggerEnter(Collider other)
        {
            if (_connected)
                return;

            _connected = true;
            GetComponent<Collider>().enabled = false;
            Collided?.Invoke(other, GetComponent<Joint>());
        }
    }
}