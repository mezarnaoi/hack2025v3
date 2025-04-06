using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueTriggerMedieval : MonoBehaviour
{
    public DialogueMedieval dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManagerPrimarMedieval>().StartDialogue(dialogue);
    }
}
