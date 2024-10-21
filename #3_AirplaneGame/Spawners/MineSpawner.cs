using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    [SerializeField] private Mine _minePrefab;
    [SerializeField] private float _amountOfBombs;
    private Vector3 _randomPosition;

    private void Awake()
    {
        for (int i = 0; i < _amountOfBombs; i++)
        {
            _randomPosition = new Vector3(Random.Range(-70, 70), Random.Range(0, 100), 0);
            Mine bomb = Instantiate(_minePrefab, _randomPosition, Quaternion.identity);
        }
    }

}
