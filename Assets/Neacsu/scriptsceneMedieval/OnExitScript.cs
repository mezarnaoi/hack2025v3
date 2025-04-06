using UnityEngine;
using UnityEngine.SceneManagement;
public class OnExitScript : MonoBehaviour
{
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
         
            SceneManager.LoadScene(2);
        }
    }


}
