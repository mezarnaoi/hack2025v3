using UnityEngine;

public class PopupDialog : MonoBehaviour
{
    public GameObject panel;     // Obiectul care conține UI-ul (Panelul)
    public float duration = 3f;  // Câte secunde stă pe ecran

    void Start()
    {
        // Poți apela această funcție din alt script sau automat la start
        ShowDialog();
    }

    public void ShowDialog()
    {
        panel.SetActive(true);
        Invoke("HideDialog", duration);
    }

    void HideDialog()
    {
        panel.SetActive(false);
    }
}
