using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMinigame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToPreviousScene();
        }
    }

    public void ReturnToPreviousScene()
    {
        if (TeleportMemory.hasSavedData)
        {
            SceneManager.LoadScene(TeleportMemory.returnScene);
        }
    }
}
