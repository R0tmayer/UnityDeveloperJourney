using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Game.Scripts
{
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private ShootPool _shootPool;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _shootPool.GetBullet();
            }
        }
    }
}