using UnityEngine;

public class ChickenActivateDialogue : MonoBehaviour
{
    public GameObject colliderToActivate;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (colliderToActivate != null)
            {
                colliderToActivate.SetActive(true);

            }
        }
    }
}
