using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueTriggerCity : MonoBehaviour
{
    public DialogueCity dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManagerPrimarCity>().StartDialogue(dialogue);
    }
}
