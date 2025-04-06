using UnityEngine;

public class FarmerQuest : MonoBehaviour
{
    private bool playerNear = false;
    private bool questCompleted = false;
    public GameObject doorToOpen;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E) && QuestState.hasCaughtChicken && !questCompleted)
        {

            questCompleted = true;

            if (doorToOpen != null)
            {
                Debug.Log("AM AJUNS SA DESCHIDEM POARTA");
                DoorOpener opener = doorToOpen.GetComponent<DoorOpener>();
                if (opener != null)
                {
                    Debug.Log("Am ajuns la poartă");
                    opener.Open();
                }
                Destroy(gameObject);

            }

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
