using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<GameObject> _hearts;

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        UpdateHeartsUI(health);
    }

    private void UpdateHeartsUI(int health)
    {
        int activeHearts = _hearts.Count(p => p.activeSelf);
        int healthDelta = health - activeHearts;
        int healthDeltaAbs = Mathf.Abs(healthDelta);

        for (int i = 0; i < healthDeltaAbs; i++)
        {
            _hearts[i].SetActive(i + 1 >= health);
        }
    }

}
