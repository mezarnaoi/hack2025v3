using UnityEngine;

public class ChickenPickup : MonoBehaviour
{
    private bool playerNear = false;
    public GameObject doorToOpen;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Ai prins gaina!");
            if (doorToOpen != null)
            {
                DoorOpener opener = doorToOpen.GetComponent<DoorOpener>();
                if (opener != null)
                {
                    Debug.Log("Am ajuns la poartă");
                    opener.Open();
                }
            }

            QuestState.hasCaughtChicken = true;

            Destroy(gameObject);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            playerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            playerNear = false;
        }
    }
}
