using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float maxTime = 1800f; // 30 minutes (1800 secondes)
    public float currentTime = 0f;
    public TextMeshProUGUI timerText; // Référence à l'affichage du texte

    protected bool isRunning = true;

    protected void Start()
    {
        currentTime = 0f; // Démarre à 00:00
        UpdateTimerDisplay();
    }

    protected void Update()
    {
        if (isRunning)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= maxTime)
            {
                currentTime = maxTime; // Bloque à 30:00
                isRunning = false; // Arrête le timer
            }

            UpdateTimerDisplay();
        }
    }

    protected void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


}
