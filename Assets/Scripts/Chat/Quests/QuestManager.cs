using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [NonReorderable]
    public ArrayList questsActiveList;
    public ArrayList questsCompleteList;

  
    public void ToActivateQuest(Quest quest)
    {
        if (!questsCompleteList.Contains(quest))
            questsActiveList.Add(quest);
        else
            Debug.LogError("Quest be comlpeted already.");
    }

    public bool IsActivatedQuest(Quest quest)
    {
        return questsActiveList.Contains(quest);
    }

    public void ToCompleteQuest(Quest quest)
    {
        if (questsActiveList.Contains(quest))
        {
            questsActiveList.Remove(quest);
            questsCompleteList.Add(quest);
        }
    }

    public bool IsCompletedQuest(Quest quest)
    {
        return (questsCompleteList.Contains(quest));
    }
  

}
