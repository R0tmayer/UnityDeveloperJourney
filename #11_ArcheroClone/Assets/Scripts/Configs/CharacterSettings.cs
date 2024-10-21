using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Configs", fileName = "CharacterSettings")]

    public class CharacterSettings : ScriptableObject
    {
        [SerializeField] private float _speedValue;
        [SerializeField] private float _shootingPeriod;
        [SerializeField] private float _bulletSpeedValue;
        [SerializeField] private float _bulletPoolCapacity;


        public float SpeedValue => _speedValue;
        public float ShootingPeriod => _shootingPeriod;
        public float BulletSpeedValue => _bulletSpeedValue;
        public float BulletPoolCapacity => _bulletPoolCapacity;

    }
}
