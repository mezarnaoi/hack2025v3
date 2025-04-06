using UnityEngine;
using UnityEngine.SceneManagement; // 🔁 trebuie adăugat pentru scene

public class ShowPanelOnTrigger1 : MonoBehaviour
{
    public GameObject panel1;
    public float displayTime1 = 3f;
    public int sceneToLoad1;  // numele scenei în build settings

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panel1.SetActive(true);
            Invoke("HidePanelAndChangeScene", displayTime1);
        }
    }

    void HidePanelAndChangeScene()
    {
        panel1.SetActive(false);
        SceneManager.LoadScene(sceneToLoad1);
    }
}
