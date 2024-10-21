using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Text _displayedCoins;
    [SerializeField] private int _coinAmount;

    private List<Coin> _coins = new List<Coin>();
    private Vector3[] _spawnPositions = { new Vector3(-5, 1, 0), new Vector3(0, 1, 0), new Vector3(5, 1, 0) };
    private int _totalCoinsAmount;
    private int _collectedCoinsAmount;

    public event UnityAction AllCoinsCollected;

    private void Awake()
    {
        for (int i = 0; i < _coinAmount; i++)
        {
            int randomIndex = Random.Range(0, _spawnPositions.Length);
            Vector3 spawnPosition = _spawnPositions[randomIndex] + new Vector3(0, 0, -i*20);
            var newCoin  = Instantiate(_coinPrefab, spawnPosition, Quaternion.identity);
            _coins.Add(newCoin);
        }

        _totalCoinsAmount = _coins.Count;
        _displayedCoins.text = 0 + " / " + _totalCoinsAmount;
    }

    public Coin GetClosestCoin(Vector3 playerPosition)
    {
        float minDistance = Mathf.Infinity;
        float distance;
        Coin closestCoin = null;

        for (int i = 0; i < _coins.Count; i++)
        {
            distance = Vector3.Distance(playerPosition, _coins[i].transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestCoin = _coins[i];
            }
        }

        return closestCoin;
    }

    public void CollectCoin(Coin coin)
    {
        if(_coins.Count <= 0)
        {
            AllCoinsCollected?.Invoke();
            return;
        }
        
        _coins.Remove(coin);
        Destroy(coin.gameObject);
        _collectedCoinsAmount++;
        _displayedCoins.text = _collectedCoinsAmount  + " / " + _totalCoinsAmount;
    }

}
