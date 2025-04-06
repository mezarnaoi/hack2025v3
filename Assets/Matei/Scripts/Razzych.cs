using UnityEngine;

public class ActivateAndDeactivateOnTrigger : MonoBehaviour
{
    public GameObject objectToActivate;
    public GameObject objectToDeactivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (objectToActivate != null)
                objectToActivate.SetActive(true);

            if (objectToDeactivate != null)
                objectToDeactivate.SetActive(false);
        }
    }
}
