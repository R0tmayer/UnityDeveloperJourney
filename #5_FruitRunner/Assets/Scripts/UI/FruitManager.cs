using System;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    private Player _player;
    private VictoryConditions _victoryConditions;

    public int CollectedFruitsAmount { get; private set; }

    public event Action<int> FruitsAmountChanged;
    public event Action CollectedFruitsGoalReached;

    private void Awake()
    {
        _victoryConditions = FindObjectOfType<VictoryConditions>();
        _player = FindObjectOfType<Player>();
        CollectedFruitsAmount = 0;
    }

    private void OnEnable()
    {
        _player.FruitCollected += OnFruitCollected;
    }

    private void OnDisable()
    {
        _player.FruitCollected -= OnFruitCollected;
    }

    private void Update()
    {
        if (CollectedFruitsAmount >= _victoryConditions.FruitsToWin)
        {
            CollectedFruitsGoalReached?.Invoke();
        }
    }

    private void OnFruitCollected(Fruit fruit)
    {
        CollectedFruitsAmount++;
        FruitsAmountChanged?.Invoke(CollectedFruitsAmount);
        Destroy(fruit.gameObject);
    }
}
