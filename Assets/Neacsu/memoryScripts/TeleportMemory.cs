using UnityEngine;

public  class TeleportMemory : MonoBehaviour
{
    public static TeleportMemory instance;


    public static Vector3 lastPosition;
    public static int returnScene = 5;
    public static bool hasSavedData = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}