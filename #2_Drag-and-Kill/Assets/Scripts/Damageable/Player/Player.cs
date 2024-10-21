using UnityEngine.Events;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable, IFloatEventable
{
    [SerializeField] private float _maxHealth;

    [SerializeField] private float _scaleChangementStep;
    [SerializeField] private uint _enemiesToLevelUp;
    [SerializeField] private float _killsLevelModifier;

    public event UnityAction PlayerDied;
    public event UnityAction<int> LevelUpped;
    public event UnityAction<float> LevelProgressChanged;
    public event UnityAction<float> ValueSetted;

    private int _level = 1;
    private float _currentHealth;
    private float _killedEnemies;
    private int _totalKills = 0;


    private void Start() => _currentHealth = _maxHealth;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeHit();

            _killedEnemies++;
            _totalKills++;

            UpdateLevelProgress();

            TryLevelUp();
        }
    }

    public void TakeHit()
    {
        _currentHealth--;
        if (_currentHealth >= 0)
        {
            ValueSetted?.Invoke(GetHealthPercent());
            if (_currentHealth == 0)
            {
                Die();
            }
        }
        else
        {
            Die();
        }
    }

    public void TakeHeal(float health)
    {
        _currentHealth += health;
        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;

        ValueSetted?.Invoke(GetHealthPercent());
    }

    private void UpdateLevelProgress()
    {
        float levelPercent = _killedEnemies / _enemiesToLevelUp;

        LevelProgressChanged?.Invoke(levelPercent);
    }

    private void Die()
    {
        if (PlayerPrefs.GetInt("Record") < _level)
            PlayerPrefs.SetInt("Record", _level);

        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + _totalKills);

        PlayerDied?.Invoke();
    }

    private void TryLevelUp()
    {
        if (_killedEnemies >= _enemiesToLevelUp)
        {
            transform.localScale += Vector3.one * _scaleChangementStep;

            _enemiesToLevelUp = (uint)(_enemiesToLevelUp * _killsLevelModifier);
            _killedEnemies = 0;

            _level++;
            LevelUpped?.Invoke(_level);

            _maxHealth += 1f;

            _currentHealth = _maxHealth;
            ValueSetted?.Invoke(GetHealthPercent());
        }
    }

    private float GetHealthPercent() => _currentHealth / _maxHealth;
}
