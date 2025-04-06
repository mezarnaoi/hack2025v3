using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Schimbă cu numele scenei tale
    }

    public void OpenSettings()
    {
        Debug.Log("Settings button clicked!");
    }

    public void ExitGame()
    {
        Application.Quit(); // Iese din joc (nu merge în editor, doar build)
    }
}