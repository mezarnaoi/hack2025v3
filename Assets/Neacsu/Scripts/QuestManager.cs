using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public string currentQuestTitle;
    public string currentQuestDescription;
    public bool questActive = false;
    public bool questCompleted = false;

    private void Start()
    {
        Debug.Log("Ti-am adaugat un quest");
        StartQuest("Test Quest", "Asta e o descriere de test pentru HUD.");
        HUDQuestUI.instance.UpdateHUD();

    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        Debug.Log("AWAKE QuestManager activ!");

    }

    public void StartQuest(string title, string description)
    {
        currentQuestTitle = title;
        currentQuestDescription = description;
        questActive = true;
        questCompleted = false;

        HUDQuestUI.instance.UpdateHUD();
    }

    public void CompleteQuest()
    {
        questCompleted = true;
        questActive = false;

        HUDQuestUI.instance.UpdateHUD();
    }
}
