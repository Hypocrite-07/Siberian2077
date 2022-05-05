using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dm;
    //public QuestManager qm;
    public GameObject ButtonAdvice;
    private bool nearTheNPC = false;
    public Dialogue dialogue;
    public Quest quest;

    private bool isNothingsText = false;
    private bool questItemTargetInit = false;
    private bool questItemsTargetInit = false;
    private bool questTalkTargetInit = false;
    private DialogueUIManager DUIM;

    public event OnTriggerDialogue onTriggerTDialogue;
    //public event OnEndDialogue onEndDialogue;

    public delegate void OnTriggerDialogue(DialogueTrigger dt);
    //public delegate void OnEndDialogue(DialogueTrigger dt);


    public void TriggerDialogue()
    {
        onTriggerTDialogue?.Invoke(this);
        if (gameObject.name == "DoorToStreet")
        {
            if(quest.isComplete)
            {
                FindObjectOfType<WorldController>().GoToStreet();
                FindObjectOfType<AudioController>().StopPetuhi();
            }    
        }

        if (gameObject.name == "DoorToHome")
        {
            if (quest.isComplete)
            {
                FindObjectOfType<WorldController>().GoToHome();
            }
        }

        if (gameObject.GetComponent<QuestDialogue>() != null) 
        {
            if (!gameObject.GetComponent<QuestDialogue>().isTalked)
            { 
                Debug.Log("TD: return");
                return;
            }
        }
        if (quest.givedQuest)
        {
            if (quest.typeOfQuest == questType.findQuest)
            {
                if (questItemTargetInit)
                {
                    if (quest.objectOfFind.GetComponent<QuestItem>().isFind)
                    {
                        questItemTargetInit = false;
                        quest.isComplete = true;
                        dialogue.isLoop = false;
                        quest.givedQuest = false;
                        Debug.Log("TD: 1");
                        Destroy(quest.objectOfFind.GetComponent<QuestItem>());
                        //FindObjectOfType<DialogueUIManager>().toShowNotification($"Квест \"{quest.name}\" был завершён.");
                    }
                }
                else
                {
                    if (!quest.isComplete)
                        questItemTargetInit = true;
                }
            }
            else if (quest.typeOfQuest == questType.findManyQuest)
            {
                if (questItemsTargetInit)
                {
                    if (quest.objectOfFind.GetComponent<QuestItem>().isFind)
                    {
                        questItemTargetInit = false;
                        quest.isComplete = true;
                        dialogue.isLoop = false;
                        quest.givedQuest = false;
                        Debug.Log("TD: 1");
                        Destroy(quest.objectOfFind.GetComponent<QuestItem>());
                        //FindObjectOfType<DialogueUIManager>().toShowNotification($"Квест \"{quest.name}\" был завершён.");
                    }
                }
                else
                {
                    if (!quest.isComplete)
                        questItemTargetInit = true;
                }
            }
            else if (quest.typeOfQuest == questType.talkQuest)
            {
                if (questTalkTargetInit)
                {
                    if (quest.objectOfTalk.GetComponent<QuestDialogue>().isTalked)
                    {
                        questTalkTargetInit = false;
                        quest.isComplete = true;
                        dialogue.isLoop = false;
                        quest.givedQuest = false;
                        Debug.Log("TD: 2");
                        Destroy(quest.objectOfTalk.GetComponent<QuestDialogue>());
                        //FindObjectOfType<DialogueUIManager>().toShowNotification($"Квест \"{quest.name}\" был завершён.");
                    }
                }
                else
                {
                    if (!quest.isComplete)
                        questTalkTargetInit = true;
                }
            }
        }
        if (dialogue != null)
        {
            if (dialogue.isReadable)
            {
                if (dialogue.isLoop)
                {
                    if (quest.isActive)
                    {
                        dm.StartDialogue(dialogue, quest);
                        QuestTargetInit();
                    }
                    else
                        dm.StartDialogue(dialogue);
                }
                else
                {
                    if (quest.isActive)
                    {
                        dm.StartDialogueQuestOnly(dialogue, quest);
                        QuestTargetInit();
                    }
                    else
                    {
                        DUIM.toShowNotification("Кажется, диалоги закончились.");
                        isNothingsText = true;
                        Debug.LogWarning("Quests are null and dialogues by readable.");
                    }
                }
            }
            else
            {
                if (quest.isActive)
                {
                    dm.StartDialogue(dialogue, quest);
                    QuestTargetInit();
                }
                else
                    dm.StartDialogue(dialogue);
                dialogue.isReadable = true;
            }
        }
    }

    private void QuestTargetInit()
    {
        
        if (!quest.isComplete)
        {
            if (quest.objectOfFind != null || quest.objectOfTalk != null)
            {
                if (quest.typeOfQuest == questType.findQuest)
                {
                    if (!questItemTargetInit)
                    {
                        //DUIM.toShowNotification($"Квест \"{quest.name}\" был начат.");
                        quest.objectOfFind.AddComponent<QuestItem>();
                        questItemTargetInit = true;
                        quest.givedQuest = true;
                    }
                }
                else if (quest.typeOfQuest == questType.findManyQuest)
                {
                    if (!questItemsTargetInit)
                    {
                        int finds = 0;
                        for (int i = 0; i < quest.objectsOfFind.Length; i++)
                        {
                            if (quest.objectsOfFind[i].GetComponent<QuestItem>().isFind)
                            {
                                finds++;
                            }
                        }
                        if (finds == quest.objectsOfFind.Length)
                            questItemsTargetInit = true;
                        else
                            questItemsTargetInit = false;
                    }
                }
                else if (quest.typeOfQuest == questType.talkQuest)
                {
                    if (!questTalkTargetInit)
                    {
                        //DUIM.toShowNotification($"Квест \"{quest.name}\" был начат.");
                        quest.objectOfTalk.AddComponent<QuestDialogue>().dialogue = quest.talk_quest;
                        questTalkTargetInit = true;
                        quest.givedQuest = true;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            SetNearTheNPC(true);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SetNearTheNPC(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SetNearTheNPC(false);
    }

    private void Awake()
    {
        DUIM = FindObjectOfType<DialogueUIManager>();
        DUIM.toShowNotification("Задание: Проснуться");
    }

    private void FixedUpdate()
    {
        if (!gameObject.CompareTag("NPC_Invis"))
        {
            if (!FindObjectOfType<Player>().isTalk)
            {
                if (!isNothingsText)
                {
                    if (nearTheNPC)
                    {
                        ButtonAdvice.gameObject.SetActive(true);
                    }
                    else
                    {
                        if (ButtonAdvice.active)
                            ButtonAdvice.gameObject.SetActive(false);
                    }
                }
                else
                {
                    ButtonAdvice.gameObject.SetActive(false);
                }
            }
            else
            {
                ButtonAdvice.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        if ((nearTheNPC && Input.GetKeyDown(KeyCode.E)))
        {
            //FindObjectOfType<AudioController>().LaunchPetuhi();
            DialogueTrigger dt = GetComponent<DialogueTrigger>();
            dt.TriggerDialogue();
        }
        if (gameObject.name == "MotherInvisible" && FindObjectOfType<Player>().isDream && Input.GetKeyDown(KeyCode.F))
        {
            FindObjectOfType<AudioController>().LaunchPetuhi();
            FindObjectOfType<CameraController>().toHideText();
            DialogueTrigger dt = GetComponent<DialogueTrigger>();
            dt.TriggerDialogue();
        }
    }

    private void SetNearTheNPC(bool value)
    {
        nearTheNPC = value;
    }
}
