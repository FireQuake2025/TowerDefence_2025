using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Health health;
    [SerializeField] private Image healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (health != null)
        {
            health.OnHealthChange += UpdateHealthBar;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }
}
