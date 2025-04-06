using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class colliderInitial : MonoBehaviour {
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    private int index = 0;
    private bool playerNear = false;
    private bool showingText = false;

    void Update() {
        if (playerNear && Input.GetKeyDown(KeyCode.E)) {
            dialogueUI.SetActive(true);
        } else if (playerNear && Input.GetKeyDown(KeyCode.Q))
            dialogueUI.SetActive(false);

    }

    void OnTriggerEnter(Collider other) {
        if (other.name == "Player") {
            playerNear = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            playerNear = false;
            dialogueUI.SetActive(false);
            index = 0;
            showingText = false;
        }
    }
}