using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameEnterTrigger : MonoBehaviour
{
    public int returnSceneIndex = 5;
    public int minigameSceneIndex = 6;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportMemory.lastPosition = other.transform.position - other.transform.forward * 2f;
            TeleportMemory.returnScene = returnSceneIndex;
            TeleportMemory.hasSavedData = true;

            SceneManager.LoadScene(minigameSceneIndex);
        }
    }
}
