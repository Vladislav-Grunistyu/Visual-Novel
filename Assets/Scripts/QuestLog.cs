using UnityEngine;
using TMPro;
using Naninovel;

public class QuestLog : MonoBehaviour
{
    private TextMeshProUGUI questText;

    private string questLog;
    public string QuestLogText
    {
        get => questLog;
        set
        {
            if (questLog != value)
            {
                questLog = value;
                UpdateQuestLogAsync(questLog).Forget();
            }
        }
    }

    private void OnEnable()
    {
        questText = GetComponent<TextMeshProUGUI>();
    }

    public async UniTaskVoid UpdateQuestLogAsync(string quest)
    {
        if (quest != null)
        {
            questText.text = "";
            foreach (char c in quest)
            {
                questText.text += c;
                await UniTask.Delay(50);
            }
            questText.text = quest;
        }
    }
}
