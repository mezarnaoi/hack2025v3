using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject soundPanel;
    public GameObject creditsPanel;
    public GameObject statsPanel;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                OpenPauseMenu();
            else
                ResumeGame();
        }
    }

    public void OpenPauseMenu()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // oprește jocul
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        CloseAllSubPanels();
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // schimbă cu numele scenei tale de meniu
    }

    public void ShowSound()
    {
        CloseAllSubPanels();
        soundPanel.SetActive(true);
    }

    public void ShowCredits()
    {
        CloseAllSubPanels();
        creditsPanel.SetActive(true);
    }

    public void ShowStats()
    {
        CloseAllSubPanels();
        statsPanel.SetActive(true);
    }

    private void CloseAllSubPanels()
    {
        soundPanel.SetActive(false);
        creditsPanel.SetActive(false);
        statsPanel.SetActive(false);
    }
}
