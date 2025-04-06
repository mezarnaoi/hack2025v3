using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToScene : MonoBehaviour
{
    public int sceneToLoad = 5;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportMemory.lastPosition = other.transform.position - other.transform.forward * 2f;
            TeleportMemory.hasSavedData = true;

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}