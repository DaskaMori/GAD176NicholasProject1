using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class HealthComponent : MonoBehaviour, IDamageable
{
    [Header("Health Settings")] 
    [SerializeField] private float maxHealth = 100.0f;
    private float _currentHealth;
    public bool IsAlive => _currentHealth > 0f;
    public event Action OnDeath;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (!IsAlive) return;
        _currentHealth -= amount;
        if (_currentHealth <= 0f)
        {
            _currentHealth = 0f;
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke();
        // gameObject.SetActive(false)
    }

    public void Heal(float amount)
    {
        _currentHealth = Mathf.Min(_currentHealth + amount, maxHealth);
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }
}
