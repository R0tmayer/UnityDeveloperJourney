using UnityEngine;

public class VictoryConditions : MonoBehaviour
{
    [SerializeField] private int _fruitsToWin;
    [SerializeField] private int _scorePointsToWin;

    public int FruitsToWin { get; private set; }
    public int ScorePointsToWin { get; private set; }

    private void Awake()
    {
        FruitsToWin = _fruitsToWin;
        ScorePointsToWin = _scorePointsToWin;
    }
}
