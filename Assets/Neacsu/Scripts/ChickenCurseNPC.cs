using UnityEngine;
using UnityEngine.AI; // dacă folosești NavMesh
using Cinemachine;

public class ChickenCurseNPC : MonoBehaviour
{
    public GameObject originalPlayer;
    public GameObject chickenPlayerPrefab;
    public CinemachineVirtualCamera vm;
    public Transform lookposition;
    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            // Dacă player-ul original e deja dezactivat, înseamnă că e cocoș -> nu îl retransformăm
            //if (!originalPlayer.activeSelf)
            //{
            //    Debug.Log("❌ Ești deja cocoș. Nu te mai blestem din nou.");
            //    return;
            //}

            Debug.Log("🔮 Te-am transformat în cocoș!");

            Vector3 pos = originalPlayer.transform.position;
            Quaternion rot = originalPlayer.transform.rotation;

            originalPlayer.SetActive(false);

            GameObject newChicken = Instantiate(chickenPlayerPrefab, pos, rot);
            newChicken.name = "Player_Chicken";

            foreach (Transform child in newChicken.transform)
            {
                if (child.name == "PlayerCameraRoot")
                {
                    vm.Follow = child;
                    break;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }
}
