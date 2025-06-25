using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;

    [SerializeField] private float currentHealth;
    
    public bool IsAlive => currentHealth > 0f;

    public event Action<float, float> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    // If tweaking maxHealth in the Inspector, keep currentHealth valid
#if UNITY_EDITOR
    private void OnValidate()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
#endif

    public void TakeDamage(float amount)
    {
        if (!IsAlive || amount <= 0f) return;
        SetHealth(currentHealth - amount);
    }

    public void Heal(float amount)
    {
        if (!IsAlive || amount <= 0f) return;
        SetHealth(currentHealth + amount);
    }

    private void SetHealth(float newHealth)
    {
        currentHealth = Mathf.Clamp(newHealth, 0f, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0f)
            Die();
    }

    private void Die()
    {
        OnDeath?.Invoke();
        gameObject.SetActive(false);
        // Destroy(gameObject);
    }

    // Expose for debugging/UI binding if you prefer
    public float GetCurrentHealth() => currentHealth;
}