using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartOnCollision : MonoBehaviour
{
    public string parkingTag = "ParkingSpot";

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(parkingTag))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
