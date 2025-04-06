using UnityEngine;

public class PotionPickup : MonoBehaviour
{
    public GameObject chickenPlayer;
    public GameObject originalPlayer;
    public Transform respawnPoint;

    private bool playerNearby = false;

    void Update()
    {
       
    }

    void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("🍵 Ai băut poțiunea și ai redevenit om!");

        originalPlayer.SetActive(true);
        originalPlayer.transform.position = respawnPoint.position;

        chickenPlayer.SetActive(false);

        Destroy(gameObject);
    }

  
}
