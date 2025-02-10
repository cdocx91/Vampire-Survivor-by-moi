using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ExpUI : MonoBehaviour
{
    public Image Fill; // Référence à la barre d'expérience (slider ou fill)
    public PlayerLevelSystem playerLevelSystem; // Référence au script de niveau
    public float fillSpeed = 5f;
    private float targetFill = 0f;
    public TextMeshProUGUI levelText; // Texte du niveau

    void Start()
    {
        // Initialiser la barre
        if (playerLevelSystem != null)
        {
            UpdateXPBar();
        }
    }

    void Update()
    {
        // Mettre à jour la barre d'expérience
        UpdateXPBar();
    }

    public void UpdateXPBar()
    {
        if (playerLevelSystem != null)
        {
            // Calculer le pourcentage de progression
            float fillAmount = (float)playerLevelSystem.currentXP / playerLevelSystem.xpToNextLevel;
            targetFill = (float)playerLevelSystem.currentXP / playerLevelSystem.xpToNextLevel;
            Fill.fillAmount = Mathf.Lerp(Fill.fillAmount, targetFill, Time.deltaTime * fillSpeed);

            // Mettre à jour le slider ou l'image
            if (Fill != null)
            {
                Fill.fillAmount = fillAmount;
            }
        }
    }
    public void UpdateXPText()
    {
        // Mettre à jour le texte du niveau
        if (levelText != null)
        {
            levelText.text = $"Level: {playerLevelSystem.level}";
        }
    }
}
