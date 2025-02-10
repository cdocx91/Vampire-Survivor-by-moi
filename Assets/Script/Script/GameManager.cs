using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float maxTime = 1800f; // 30 minutes (1800 secondes)
    public float currentTime = 0f;
    public TextMeshProUGUI timerText; // R�f�rence � l'affichage du texte

    protected bool isRunning = true;

    protected void Start()
    {
        currentTime = 0f; // D�marre � 00:00
        UpdateTimerDisplay();
    }

    protected void Update()
    {
        if (isRunning)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= maxTime)
            {
                currentTime = maxTime; // Bloque � 30:00
                isRunning = false; // Arr�te le timer
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
