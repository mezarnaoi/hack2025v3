using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class script : MonoBehaviour
{
    public GameObject settingsPanel;     // Panoul de setări
    public GameObject creditsPanel;      // Panoul care conține creditele
    public GameObject settingsPanel1;
    private bool isMuted = false;        // Pentru a alterna mute/unmute

    public void StartGame()
    {
        SceneManager.LoadScene(1); // Poți schimba cu numele scenei
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        StartCoroutine(ShowCreditsCoroutine());
    }

    IEnumerator ShowCreditsCoroutine()
    {
        settingsPanel1.SetActive(false);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(true);
        yield return new WaitForSeconds(5f);
        creditsPanel.SetActive(false);
        settingsPanel1.SetActive(true);
        settingsPanel.SetActive(true);
        
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0f : 1f;
        Debug.Log("Mute status: " + isMuted);
    }
}
