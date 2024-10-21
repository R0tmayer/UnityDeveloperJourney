using Assets.Scripts.Configs;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shooting
{
    [RequireComponent(typeof(Rigidbody))]

    public class Bullet : MonoBehaviour
    {
        [SerializeField] [Required] [AssetSelector] private CharacterSettings _settings;
        private Rigidbody _rigidbody;

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        public void MoveInDirection(Vector3 direction) => _rigidbody.velocity = direction * _settings.BulletSpeedValue;

        private void OnCollisionEnter(Collision collision)
        {
            gameObject.SetActive(false);
        }
    }
}
