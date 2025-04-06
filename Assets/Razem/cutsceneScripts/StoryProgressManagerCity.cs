using UnityEngine;

public class StoryProgressManagerCity : MonoBehaviour
{
    public static StoryProgressManagerCity instance;

    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

   
}
