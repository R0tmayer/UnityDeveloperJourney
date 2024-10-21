using System.Collections.Generic;
using Assets.Scripts.Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.Shooting
{
    public class EnemySearcher : MonoBehaviour
    {
        [SerializeField] [Required] private List<Enemy> _enemies;

        public Enemy ClosestEnemy { get; private set; }

        private void Update() => SearchClosestEnemy();

        private void SearchClosestEnemy()
        {
            var closestDistance = float.MaxValue;

            foreach (Enemy enemy in _enemies)
            {
                if (!enemy.gameObject.activeInHierarchy) continue;

                var distance = Vector3.Distance(enemy.transform.position, transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    ClosestEnemy = enemy;
                }
            }
        }
    }
}