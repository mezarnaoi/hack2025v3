using UnityEngine;

public class ReturnPlayerToLastPos : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        if (TeleportMemory.hasSavedData && player != null)
        {
            player.transform.position = TeleportMemory.lastPosition;
        }
    }
}
