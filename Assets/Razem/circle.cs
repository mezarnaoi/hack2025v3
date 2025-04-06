using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Circle : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue(int step)
    {
        FindObjectOfType<DialogueManagerPrimar>().StartDialogue(dialogue, step);
    }
}
