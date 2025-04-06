using UnityEngine;
using TMPro;

public class HUDQuestUI : MonoBehaviour
{
    public static HUDQuestUI instance;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateHUD()
    {
        if (QuestManager.instance.questActive)
        {
            titleText.text = "❗ " + QuestManager.instance.currentQuestTitle;
            descriptionText.text = QuestManager.instance.currentQuestDescription;
        }
        else if (QuestManager.instance.questCompleted)
        {
            titleText.text = "✔️ " + QuestManager.instance.currentQuestTitle;
            descriptionText.text = "Quest completat!";
        }
        else
        {
            titleText.text = "";
            descriptionText.text = "";
        }
    }
}
