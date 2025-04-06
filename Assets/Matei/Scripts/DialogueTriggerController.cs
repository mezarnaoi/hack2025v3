using UnityEngine;

public class DialogueTriggerController : MonoBehaviour
{
    public GameObject triggerObject;  
    public int requiredStep;

    private bool activated = false;

    void Update()
    {
        if (!activated && StoryProgressManager.instance.currentStep == requiredStep)
        {
            triggerObject.SetActive(true);
            activated = true;
        }
    }
}
