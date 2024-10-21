using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float _timeHideDeadEnemy;

    public event UnityAction EnemyDied;
    public event UnityAction EnemyAppeared;

    private const float _enemyAppearDelay = 0.1f;
    
    private bool _isAlive = true;



    private void OnEnable()
    {
        _isAlive = true;

        Invoke(nameof(EnemyAppear), _enemyAppearDelay);
    }

    public void TakeHit()
    {
        if (_isAlive)
        {
            _isAlive = false;

            StartCoroutine(DieCoroutine());
        }
    }

    private void EnemyAppear() => EnemyAppeared?.Invoke();

    private IEnumerator DieCoroutine()
    {
        EnemyDied?.Invoke();

        yield return new WaitForSeconds(_timeHideDeadEnemy);

        gameObject.SetActive(false);
    }
}
