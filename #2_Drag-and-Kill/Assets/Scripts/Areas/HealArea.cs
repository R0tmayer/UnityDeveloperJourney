using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class HealArea : MonoBehaviour, IFloatEventable
{
    [SerializeField] private PlayerDetector _detectArea;
    [SerializeField] private float _maxHealCapacity;
    [SerializeField] private float _healStep;
    [SerializeField] private float _healPeriod;
    [SerializeField] private float _dieDelay;

    public event UnityAction<float> ValueSetted;

    private float _healCapacity;

    private IEnumerator _healCoroutine;


    private void OnEnable()
    {
        _detectArea.PlayerTriggered += TryHeal;

        _healCapacity = _maxHealCapacity;

        Invoke(nameof(SendStartEvent), 0.01f);
    }

    private void OnDisable() => _detectArea.PlayerTriggered -= TryHeal;

    private void TryHeal(Player player)
    {
        if (_healCoroutine != null)
            StopCoroutine(_healCoroutine);

        if (player != null)
        {
            _healCoroutine = Heal(player);
            StartCoroutine(_healCoroutine);
        }
    }

    private void SendStartEvent() => ValueSetted?.Invoke(1f);

    private void Die() => gameObject.SetActive(false);

    private float GetHeal()
    {
        float heal;
        if (_healCapacity >= _healStep)
        {
            _healCapacity -= _healStep;
            heal = _healStep;
        }
        else
        {
            heal = _healCapacity;
            _healCapacity = 0f;

            Invoke(nameof(Die), _dieDelay);
        }

        ValueSetted?.Invoke(GetHealthCapacityPercent());
        return heal;
    }

    private float GetHealthCapacityPercent() => _healCapacity / _maxHealCapacity;

    private IEnumerator Heal(Player player)
    {
        while (_healCapacity > 0)
        {
            player.TakeHeal(GetHeal());

            yield return new WaitForSeconds(_healPeriod);
        }

        _healCoroutine = null;
    }
}
