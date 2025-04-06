using UnityEngine;

public class ShowPanelOnTrigger : MonoBehaviour
{
    public GameObject panel;
    public float displayTime = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panel.SetActive(true);
            Invoke("HidePanel", displayTime);
        }
    }

    void HidePanel()
    {
        panel.SetActive(false);
    }
}
