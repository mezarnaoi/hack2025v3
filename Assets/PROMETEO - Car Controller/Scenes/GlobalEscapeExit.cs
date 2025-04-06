using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalEscapeExit : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(5);
        }
    }
}
