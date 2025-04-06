using UnityEngine;

public class StoryProgressManagerMedieval : MonoBehaviour
{
    public static StoryProgressManagerMedieval instance;

    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

   
}
