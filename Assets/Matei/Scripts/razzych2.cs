using UnityEngine;

public class TriggerExitActivator : MonoBehaviour
{
    public GameObject objectToActivate;
    public GameObject objectToDeactivate;

    private bool playerIsInTrigger = false; // Verificăm dacă jucătorul este în trigger
    private bool rewindPressed = false; // Verificăm dacă R a fost apăsată în timpul ieșirii

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerIsInTrigger && Input.GetKey(KeyCode.R)) // Verificăm dacă tasta R este apăsată
            {
                rewindPressed = true;
            }
            playerIsInTrigger = false; // Playerul a ieșit din trigger
        }
    }

    private void Update()
    {
        if (rewindPressed) // Dacă R este apăsată și playerul a ieșit
        {
            if (objectToActivate != null)
                objectToActivate.SetActive(true);

            if (objectToDeactivate != null)
                objectToDeactivate.SetActive(false);

            rewindPressed = false; // Resetăm pentru a nu se repeta
        }
    }
}