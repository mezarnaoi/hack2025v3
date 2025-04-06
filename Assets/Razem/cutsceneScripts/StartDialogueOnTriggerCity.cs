using UnityEngine;

public class StartDialogueOnTriggerCity : MonoBehaviour
{
    public DialogueTriggerCity dialogueTrigger;
    public GameObject exclamationMark;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
        
                triggered = true;
                dialogueTrigger.TriggerDialogue();
                
                exclamationMark.SetActive(false);
                
                gameObject.SetActive(false); 
            
        }
    }
}
