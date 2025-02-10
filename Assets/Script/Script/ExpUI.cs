using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ExpUI : MonoBehaviour
{
    public Image Fill; // R�f�rence � la barre d'exp�rience (slider ou fill)
    public PlayerLevelSystem playerLevelSystem; // R�f�rence au script de niveau
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
        // Mettre � jour la barre d'exp�rience
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

            // Mettre � jour le slider ou l'image
            if (Fill != null)
            {
                Fill.fillAmount = fillAmount;
            }
        }
    }
    public void UpdateXPText()
    {
        // Mettre � jour le texte du niveau
        if (levelText != null)
        {
            levelText.text = $"Level: {playerLevelSystem.level}";
        }
    }
}
