using UnityEngine.Events;
using UnityEngine;
using TMPro;

public class ProgressDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text[] _currentLevel;
    [SerializeField] private TMP_Text[] _nextLevel;

    public event UnityAction ProgressUpdated;


    private void OnEnable() => _player.LevelUpped += ChangeLevel;

    private void OnDisable() => _player.LevelUpped -= ChangeLevel;

    private void ChangeLevel(int currentLevel)
    {
        foreach (TMP_Text text in _currentLevel)
            text.text = currentLevel.ToString();

        foreach (TMP_Text text in _nextLevel)
            text.text = (currentLevel + 1).ToString();

        ProgressUpdated?.Invoke();
    }
}
