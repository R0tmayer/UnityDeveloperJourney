using System;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemies;
    public event Action AllEnemiesDied;

    private int _deadEnemiesCount;
    private int _index = -1;

    public Enemy AliveEnemy
    {
        get
        {
            _index++;
            return _enemies[_index];
        }
    }

    private void Start()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.Died += OnEnemyDied;
        }
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _deadEnemiesCount++;
        enemy.Died -= OnEnemyDied;

        if (_deadEnemiesCount == _enemies.Length)
        {
            AllEnemiesDied?.Invoke();
        }
    }
}
