using UnityEngine;

public class StartDialogueOnTrigger : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;
    public int requiredStep = 0;
    public GameObject exclamationMark;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            if (StoryProgressManager.instance.currentStep == requiredStep)
            {
                triggered = true;
                dialogueTrigger.TriggerDialogue(requiredStep);
                StoryProgressManager.instance.AdvanceStep();
                
                exclamationMark.SetActive(false);
                
                gameObject.SetActive(false); 
            }
        }
    }
}
