using UnityEngine;

public class DialogueTriggerControllerMedieval : MonoBehaviour
{
    public GameObject triggerObject;  
    public int requiredStep;

    private bool activated = false;

    void Update()
    {
        if (!activated)
        {
            triggerObject.SetActive(true);
            activated = true;
        }
    }
}
