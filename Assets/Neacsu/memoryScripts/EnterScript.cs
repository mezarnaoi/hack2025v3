using UnityEngine;
using UnityEngine.SceneManagement;


public class EnterScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            SceneManager.LoadScene(11);
        }
    }

}
