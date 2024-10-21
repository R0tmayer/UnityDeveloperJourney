using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private float _amountOfCoins;
    private Vector3 _randomPosition;

    private void Awake()
    {
        for (int i = 0; i < _amountOfCoins; i++)
        {
            _randomPosition = new Vector3(Random.Range(-70, 70), Random.Range(0, 100), 0);
            Coin coin = Instantiate(_coinPrefab, _randomPosition, Quaternion.identity);
            coin.transform.eulerAngles = new Vector3(90, 0, 0);
        }
    }

}
