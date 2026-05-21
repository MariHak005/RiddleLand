using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneButtons : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}