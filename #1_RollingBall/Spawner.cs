using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private float _secondsBetweenSpawn;

    private float _timer;

    private void Start()
    {
        InitializePool(_enemyPrefabs); 
    }   

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _secondsBetweenSpawn)
        {
            if(TryGetObject(out GameObject enemy))
            {
                _timer = 0;
                int spawnPointNumber = Random.Range(0, _spawnPoints.Length);
                ActivateEnemy(enemy, _spawnPoints[spawnPointNumber].position);
            }
        }
    }

    private void ActivateEnemy(GameObject enemy, Vector3 spawnPointPosition)
    {
        enemy.SetActive(true);
        enemy.transform.position = spawnPointPosition;
    }
}
