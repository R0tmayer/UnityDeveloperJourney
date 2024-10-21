using System;
using System.Collections;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;

namespace _Game.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        private Rigidbody _rigidbody;

        private Action<Bullet> _disableAction;
        public event Action<Bullet> Disabled;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Move()
        {
            _rigidbody.velocity = transform.forward * _moveSpeed;
            StartCoroutine(DisableBulletAfterDelay());
        }
        
        private IEnumerator DisableBulletAfterDelay()
        {
            yield return new WaitForSeconds(3);
            Disabled?.Invoke(this);
        }

        public void Init(Action<Bullet> killAction)
        {
            _disableAction = killAction;
        }

        private void OnCollisionEnter(Collision collision)
        {
            _disableAction(this);
        }
    }
}