using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [Header("Volume")]
    public Slider volumeSlider;

    [Header("Qualité Graphique")]
    public TMP_Dropdown graphicsDropdown;

    [Header("Autres Menus")]
    public GameObject mainMenu;
    public GameObject optionsMenu;

    private void Start()
    {
        // Charger les paramètres sauvegardés
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        graphicsDropdown.value = PlayerPrefs.GetInt("GraphicsQuality", 2);

        // Appliquer les paramètres
        ApplyVolume(volumeSlider.value);
        ApplyGraphicsQuality(graphicsDropdown.value);

        // Ajouter des listeners
        volumeSlider.onValueChanged.AddListener(ApplyVolume);
        graphicsDropdown.onValueChanged.AddListener(ApplyGraphicsQuality);
    }

    public void ApplyVolume(float volume)
    {
        AudioListener.volume = volume; // Applique le volume global
        PlayerPrefs.SetFloat("Volume", volume); // Sauvegarde
    }

    public void ApplyGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex); // Change la qualité graphique
        PlayerPrefs.SetInt("GraphicsQuality", qualityIndex); // Sauvegarde
    }

    public void BackToMainMenu()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
