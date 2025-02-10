using System;
using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "Scriptable Objects/HealthData")]
public class HealthData : ScriptableObject
{
    public int currentHealth = 100; // Vie actuelle
    [HideInInspector]
    public int maxHealth = 100; // Vie maximale
    public event Action OnHealthChange;

    public void Initialize()
    {
        maxHealth = 100;
        currentHealth = maxHealth; // R�initialiser la vie � sa valeur maximale
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0; // �vite d'avoir une vie n�gative
        }
        OnHealthChange?.Invoke();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        UnityEngine.Debug.Log($"You took damage !! Current health: {currentHealth}");
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // �vite de d�passer la vie maximale

        }
        OnHealthChange?.Invoke();
    }

}

