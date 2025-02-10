using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Charger la premi�re sc�ne du jeu
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        // Quitter le jeu
        Debug.Log("Quitter le jeu !");
        Application.Quit();
    }
}
