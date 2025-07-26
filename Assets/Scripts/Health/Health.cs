using System;
using UnityEngine;

public class Health : MonoBehaviour
{

    public event Action<int, int> OnHealthChange;
    public int currentHealth;
    [SerializeField] private int maxHealth = 20;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public bool IsDead()
    {
        return currentHealth > 0;
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth = Mathf.Max(currentHealth - damageAmount, 0);
            OnHealthChange?.Invoke(currentHealth, maxHealth);
        }
        Debug.Log($"Current Health: {currentHealth}");
    }
}
