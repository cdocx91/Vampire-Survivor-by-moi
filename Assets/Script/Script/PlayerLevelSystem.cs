using System;
using System.Collections;
using UnityEngine;

public class PlayerLevelSystem : MonoBehaviour
{
    public int level = 1; // Niveau du joueur
    public int currentXP = 0; // Expérience actuelle
    public int xpToNextLevel = 10; // XP requis pour passer au prochain niveau
    public float xpMultiplier = 1.5f; // Facteur de croissance pour XP

    public delegate void OnLevelUp(int newLevel);
    public event OnLevelUp LevelUpEvent;

    private bool hasExpPassive = false;
    private float expPerTick = 0;

    public ExpUI expUI;

    private void Start()
    {
        expUI = FindFirstObjectByType<ExpUI>();
    }

    public void GainXP(int amount)
    {
        currentXP += amount;

        // Vérifier si le joueur a assez d'XP pour passer au niveau suivant
        while (currentXP >= xpToNextLevel)
        {
            LevelUp();
            expUI.UpdateXPText();
        }
    }

    void LevelUp()
    {
        currentXP -= xpToNextLevel;
        level++;
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * xpMultiplier);

        Debug.Log($"Level Up! Nouveau niveau : {level}");

        // Appeler le gestionnaire d'amélioration
        FindAnyObjectByType<UpgradeManager>().ShowUpgradeOptions();

        Time.timeScale = 0;

        // Déclencher l'événement de niveau supérieur
        LevelUpEvent?.Invoke(level);
    }

    public void ActivateExpPassive(float amount)
    {
        if (!hasExpPassive)
        {
            hasExpPassive = true;
            expPerTick = amount;
            StartCoroutine(PassiveExpGain());
        }
    }

    private IEnumerator PassiveExpGain()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f); // Gagne de l'EXP toutes les 3 secondes

            if (hasExpPassive)
            {
                GainXP(Mathf.RoundToInt(expPerTick));
            }
        }
    }
}