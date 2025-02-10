using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    public HealthData healthData; // Référence au ScriptableObject
    public UnityEngine.UI.Image fillImage;   // Référence à l'image
    public float updateSpeed = 0.2f; // Vitesse de transition (plus petit = plus lent)
    private float targetFillAmount; // La cible de remplissage
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (healthData != null && fillImage != null)
        {
            UpdateHealthBar(); // Initialiser la barre
        }
        healthData.OnHealthChange += UpdateHealthBar;
    }

    private void OnDestroy()
    {
        healthData.OnHealthChange -= UpdateHealthBar;
    }



    void UpdateHealthBar()
    {
        // Calculer la proportion de vie restante
        targetFillAmount = (float)healthData.currentHealth / healthData.maxHealth;
        
        // Ajuster la largeur de l'image Fill en fonction de la santé
        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, targetFillAmount, updateSpeed * Time.deltaTime);
        StopAllCoroutines(); // Empêche des conflits entre les coroutines
        StartCoroutine(AnimateHealthBar()); // Lance une transition fluide
        // Met à jour la couleur
        UpdateHealthBarColor();
    }

    void UpdateHealthBarColor()
    {
        if (fillImage != null)
        {
            // Exemple : Vert (100% santé) -> Rouge (0% santé)
            fillImage.color = Color.Lerp(Color.red, Color.green, targetFillAmount);
        }
    }

    public void SmoothUpdateHealthBar()
    {
        StartCoroutine(AnimateHealthBar());
    }
    private System.Collections.IEnumerator AnimateHealthBar()
    {
        float startFillAmount = fillImage.fillAmount;
        float elapsedTime = 0f;

        while (elapsedTime < updateSpeed)
        {
            elapsedTime += Time.deltaTime;
            fillImage.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, elapsedTime / updateSpeed);
            yield return null;
        }

        fillImage.fillAmount = targetFillAmount;
    }
}
