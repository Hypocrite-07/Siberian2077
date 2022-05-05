using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{

    public static EventManager current;

    public event Action OnEventStartDialogue;
    public event Action OnEventEndDialogue;
    public event Action OnEventTriggerDialogue;
    //public static event Action OnEvent

    /*
    private void Awake()
    {
        if (current == null)
            current = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    
    public void onStartDialogueEvent()
    {
        Debug.Log("Event Started");
        OnEventStartDialogue?.Invoke();
    }
    */

    /*
    public void onTriggerDialogueEvent(DialogueTrigger dt)
    {
        OnEventTriggerDialogue?.Invoke(dt);
        OnEventTriggerDialogue += doorsMiniScritp;
    }

    public void onEndDialogueEvent(DialogueManager dm)
    {
        OnEventStartDialogue?.Invoke(dm);
    }

    private void doorsMiniScritp(DialogueTrigger dt)
    {
        if (dt.gameObject.name == "DoorToStreet")
        {
            if (dt.quest.isComplete)
            {
                FindObjectOfType<WorldController>().GoToStreet();
                FindObjectOfType<AudioController>().StopPetuhi();
            }
        }

        if (gameObject.name == "DoorToHome")
        {
            if (dt.quest.isComplete)
            {
                FindObjectOfType<WorldController>().GoToHome();
            }
        }
    }
    */

}
