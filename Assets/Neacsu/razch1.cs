using UnityEngine;

public class WitchTrigger : MonoBehaviour
{
    public PlayerTransformation transformationScript = null;
    public PlayerTransformation1 transformationScript1 = null;
    public GameObject bomb1 = null;
    public GameObject bomb = null;
    private bool playerInZone = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            bomb1.SetActive(false);
            bomb.SetActive(false);
        }
    }

    void Update()
    {
        if (transformationScript != null && playerInZone && Input.GetKeyDown(KeyCode.E))
        {
            transformationScript.TransformIntoChicken();
            bomb1.SetActive(true);
            
            Debug.Log("Vrăjitoarea: Acum ești o găină! 🧙‍♀️🐔");
        }
        if (transformationScript1 != null && playerInZone && Input.GetKeyDown(KeyCode.E))
        {
            transformationScript1.TransformBackToHuman();
            bomb.SetActive(true);
            Debug.Log("Vrăjitoarea: Acum ești o găină! 🧙‍♀️🐔");
        }
    }
}
