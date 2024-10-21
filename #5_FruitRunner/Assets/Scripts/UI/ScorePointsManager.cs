using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePointsManager : MonoBehaviour
{
    private VictoryConditions _victoryConditions;

    public int ScorePoints { get; private set; }

    public event Action ScorePointsGoalReached;
    public event Action<int> ScorePointsChanged;

    private void Awake()
    {
        _victoryConditions = FindObjectOfType<VictoryConditions>();
        ScorePoints = 0;
    }

    private void FixedUpdate()
    {
        ScorePoints++;
        ScorePointsChanged?.Invoke(ScorePoints);

        if (ScorePoints >= _victoryConditions.ScorePointsToWin)
        {
            ScorePointsGoalReached?.Invoke();
        }
    }
}
