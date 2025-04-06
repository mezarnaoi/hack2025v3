using UnityEngine;

public class StoryProgressManager : MonoBehaviour
{
    public static StoryProgressManager instance;

    public int currentStep = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AdvanceStep()
    {
        currentStep++;
    }
}
