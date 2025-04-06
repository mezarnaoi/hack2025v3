using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC_chicken_dialogue : MonoBehaviour
{
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    private int index = 0;
    private bool playerNear = false;
    private bool showingText = false;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            //    if (!showingText)
            //    {
            //Debug.Log("AI APASAT E PE NPC!!");
            //dialogueUI.SetActive(true);

            //dialogueText.text = lines[index];
            //    showingText = true;
            //}
            //else
            //{
            //    index++;
            //    if (index < lines.Length)
            //    {
            //        dialogueText.text = lines[index];
            //    }
            //    else
            //    {
            //        dialogueUI.SetActive(false);
            //        index = 0;
            //        showingText = false;
            //    }
            //}
        }
        //else if(playerNear && Input.GetKeyDown(KeyCode.Q))
            //dialogueUI.SetActive(false);

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
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            dialogueUI.SetActive(false);
            index = 0;
            showingText = false;
        }
    }
}
