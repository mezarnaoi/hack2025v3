using UnityEngine;

public class DisableOnTrigger : MonoBehaviour
{
    public GameObject objectToDisable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            if (objectToDisable != null)
            {
                objectToDisable.SetActive(false);
            }
        }
    }
}
