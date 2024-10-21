using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Rocket _rocketPrefab;
    [SerializeField] private float _amountOfRockets;
    private Vector3 _randomPosition;

    private void Awake()
    {
        for (int i = 0; i < _amountOfRockets; i++)
        {
            _randomPosition = new Vector3(Random.Range(-70, 70), Random.Range(0, 100), 0);
            Rocket rocket = Instantiate(_rocketPrefab, _randomPosition, Quaternion.identity);
            rocket.Target = _target;
        }
    }
}
