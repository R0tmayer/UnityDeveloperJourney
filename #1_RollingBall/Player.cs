using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour
{
    [SerializeField] private CoinManager _coinManager;
    [SerializeField] private Transform _cameraCenter;
    [SerializeField] private float _torqueValue;
    [SerializeField] private int _maxHealth;

    private Rigidbody _rigidbody;
    private int _currentHealth;
    private float _multipleTorqueValue;

    public event UnityAction<int> HealthChanged;
    public event UnityAction Died;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.maxAngularVelocity = Mathf.Infinity;
        _multipleTorqueValue = _torqueValue * 3;
        HealthChanged?.Invoke(_currentHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin coin))
            _coinManager.CollectCoin(coin);

        if(other.TryGetComponent(out HealingSalve healingSalve))
        {
            Heal();
        }    
    }

    private void FixedUpdate()
    {
        Move(_torqueValue);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Move(_multipleTorqueValue);
        }
    }

    private void Move(float torqueValue)
    {
        _rigidbody.AddTorque(_cameraCenter.forward * (-1) * Input.GetAxis("Horizontal") * torqueValue);
        _rigidbody.AddTorque(_cameraCenter.right * Input.GetAxis("Vertical") * torqueValue);
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal()
    {
        if(_currentHealth >= _maxHealth)
        {
            return;
        }

        _currentHealth ++;
        HealthChanged?.Invoke(_currentHealth);
    }

    public void Die()
    {
        Died?.Invoke();
    }

    
}
