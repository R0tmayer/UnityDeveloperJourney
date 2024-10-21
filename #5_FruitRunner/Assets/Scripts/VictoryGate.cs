using System;
using UnityEngine;

public class VictoryGate : MonoBehaviour
{
    private LevelSceneController _levelSceneController;
    private Player _player;

    public event Action ReachedVictoryGate;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _levelSceneController = FindObjectOfType<LevelSceneController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            ReachedVictoryGate?.Invoke();
        }
    }
}
