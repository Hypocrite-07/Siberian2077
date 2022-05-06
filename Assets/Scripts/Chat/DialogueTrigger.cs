using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dm;
    //public QuestManager qm;
    private bool nearTheNPC = false;
    public Dialogue dialogue;
    public Quest quest;

    private GameObject ButtonAdvice;
    private bool isNothingsText = false, questItemTargetInit = false, questItemsTargetInit = false, questTalkTargetInit = false;
    //private DialogueUIManager DUIM;
    public void TriggerDialogue()
    {

        //EventManager.current.onTriggerDialogueEvent(this);
        if (gameObject.name == "DoorToStreet")
        {
            if(quest.isComplete)
            {
                WorldController.Instance.GoToStreet();
            }    
        }

        if (gameObject.name == "DoorToHome")
        {
            if (quest.isComplete)
            {
                WorldController.Instance.GoToHome();
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
                Debug.Log("DT: [ManyQuest Checker] Start");
                if (questItemsTargetInit)
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
                    {
                        questItemsTargetInit = false;
                        quest.isComplete = true;
                        dialogue.isLoop = false;
                        quest.givedQuest = false;
                        Debug.Log("TD: 1");
                        for (int i = 0; i < quest.objectsOfFind.Length; i++)
                        {
                            Destroy(quest.objectsOfFind[i].GetComponent<QuestItem>());
                        }
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
                        DialogueUIManager.ToShowNotification("Кажется, диалоги закончились.");
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
            if (quest.objectOfFind != null || quest.objectOfTalk != null || quest.objectsOfFind != null)
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
                    Debug.Log("DT: [ManyQuest] Register");
                    if (!questItemsTargetInit)
                    {
                        Debug.Log("DT: [ManyQuest] 1 Step");
                        for (int i = 0; i < quest.objectsOfFind.Length; i++)
                        {
                            Debug.Log($"DT: [ManyQuest] {i+1} of the {quest.objectsOfFind.Length} quest items was init");
                            quest.objectsOfFind[i].AddComponent<QuestItem>();
                        }
                        Debug.Log("DT: [ManyQuest] 2 Step");
                        questItemsTargetInit = true;
                        quest.givedQuest = true;
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
        //DUIM = FindObjectOfType<DialogueUIManager>();
        if (!gameObject.CompareTag("NPC_Invis"))
            ButtonAdvice = gameObject.transform.Find("Button E").gameObject;
        DialogueUIManager.ToShowNotification("Задание: Проснуться");
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
            if (!Player.Instance.isTalk)
            {
                //FindObjectOfType<AudioController>().LaunchPetuhi();
                DialogueTrigger dt = GetComponent<DialogueTrigger>();
                dt.TriggerDialogue();
            }
        }
        if (gameObject.name == "MotherInvisible" && Player.Instance.isDream && !Player.Instance.isTalk && Input.GetKeyDown(KeyCode.F))
        {
            AudioController.Instance.LaunchPetuhi();
            CameraController.Instance.toHideText();
            DialogueTrigger dt = GetComponent<DialogueTrigger>();
            dt.TriggerDialogue();
        }
    }

    private void SetNearTheNPC(bool value)
    {
        nearTheNPC = value;
    }
}
