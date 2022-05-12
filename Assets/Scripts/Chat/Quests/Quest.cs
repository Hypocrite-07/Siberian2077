using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    
    public int id;
    public string name, description, title;
    [SerializeField]
    private Message[] beforeComplete, afterComplete, afterQuestDiologue;
    public GameObject[] objectsOfFind;
    public GameObject objectOfFind;
    public GameObject objectOfTalk;

    [SerializeField]
    public Dialogue talk_quest;

    private Message[] text;

    public bool isActive, isComplete, afterReadable = false, givedQuest;
    public questType typeOfQuest;

    public Message[] getMessages()
    {
        if(!isActive) { return null; }
        if (afterReadable) { return afterQuestDiologue;  }
        if (!isComplete)
            text = beforeComplete;
        else
        {
            text = afterComplete;
            afterReadable = true;
        }
        return text;
    }

    /*
    public void EnableQuest()
    {

    }
    */

    public void CompleteQuest()
    {
        if(isActive)
        {
            isComplete = true;
        }
    }

}

public enum questType { findQuest, findManyQuest, talkQuest };
