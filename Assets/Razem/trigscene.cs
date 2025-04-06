using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTrigger : MonoBehaviour
{
    public int sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // sau scoate if-ul dacă vrei orice obiect să triggărească
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
