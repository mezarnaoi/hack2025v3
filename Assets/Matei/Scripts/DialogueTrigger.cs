using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue(int step)
    {
        FindObjectOfType<DialogueManagerPrimar>().StartDialogue(dialogue, step);
    }
}
