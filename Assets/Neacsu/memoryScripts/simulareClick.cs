using UnityEngine;
using UnityEngine.UI;

public class EnterKeyUITrigger : MonoBehaviour
{
    public Button targetButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            targetButton.onClick.Invoke(); 
        }
    }
}