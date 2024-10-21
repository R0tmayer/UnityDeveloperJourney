using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _health;

        public event Action Died;

        public void OnCollisionEnter(Collision collision)
        {
            TakeDamage(1);
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;

            if(_health <= 0)
            {
                _health = 0;
                Died?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}
