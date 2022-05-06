using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Text nameText;

    private string _localNameText;

    private Queue<Message> messages;
    private Queue<Quest> quests;

    private bool isDreamPlayerUpdate = false;

    private Dialogue finalDialogue;
    private Dialogue thisDialogue;

    private void Start()
    {
        messages = new Queue<Message>();
        quests = new Queue<Quest>();
    }

    private void Update()
    {
        if (FindObjectOfType<Player>().isDream && !isDreamPlayerUpdate && Input.GetKeyDown(KeyCode.Space))
            return;
        else if (FindObjectOfType<Player>().isDream && isDreamPlayerUpdate)
        {
            if (messages.Count >= 0 && Input.GetKeyDown(KeyCode.Space))
                DisplayNextMessage();
        }
        else
            if (messages.Count >= 0 && Input.GetKeyDown(KeyCode.Space))
                DisplayNextMessage();
    }

    private void initDefaultSettingDialogue(Dialogue dialogue)
    {
        thisDialogue = dialogue;
        FindObjectOfType<DialogueUIManager>().toShow();
        nameText.text = dialogue.name;
        _localNameText = dialogue.name;
        messages.Clear();

        StartD_Script();
    }

    public void StartDialogueQuestOnly(Dialogue dialogue, Quest quest)
    {
        initDefaultSettingDialogue(dialogue);
        foreach (Message message in quest.getMessages())
        {
            messages.Enqueue(message);
        }
        DisplayNextMessage();
    }

    public void StartDialogue(Dialogue dialogue, Quest quest)
    {
        initDefaultSettingDialogue(dialogue);

        foreach (Message message in dialogue.messages)
        {
            messages.Enqueue(message);
        }
        foreach (Message message in quest.getMessages())
        {
            messages.Enqueue(message);
        }
        DisplayNextMessage();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        initDefaultSettingDialogue(dialogue);

        foreach (Message message in dialogue.messages)
        {
            messages.Enqueue(message);
        }
        DisplayNextMessage();
    }

    public void StartFinalDialogue(Dialogue dialogue)
    {
        FindObjectOfType<DialogueUIManager>().toShow();
        finalDialogue = dialogue;
        nameText.text = dialogue.name;
        _localNameText = dialogue.name;
        messages.Clear();

        foreach (Message message in dialogue.messages)
        {
            messages.Enqueue(message);
        }
        DisplayNextMessage();
    }

    public void DisplayNextMessage()
    {
        if (Player.Instance.isDream)
            isDreamPlayerUpdate = true;
        FindObjectOfType<Player>().speedPlus = 0;
        FindObjectOfType<Player>().isTalk = true;
        if (messages.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        Message message = messages.Dequeue();
        /*
        if (finalDialogue != null)
        {
            if (finalDialogue.isFinal)
            {
                nameText.text = message.nameNpc;
                nameText.color = message.color;
            }
        }
        */
        if (message.isAuthor)
        {
            nameText.text = "";
        }
        else
        {
            if (!message.isNPC)
            {
                nameText.text = Player.Instance.MainName;
                nameText.color = Player.Instance.ColorName;
            }
            else
            {
                nameText.text = _localNameText;
                nameText.color = Color.cyan;
            }
        }
        if (message.notificationText.Length != 0)
        {
            DialogueUIManager.ToShowNotification(message.notificationText);
        }
        dialogueText.text = message.messageText;
    }

    public void EndDialogue()
    {
        //EventManager.current.onEndDialogueEvent(this);
        if (Player.Instance.isDream)
            CameraController.Instance.toHideBlack();
        Player.Instance.isTalk = false;
        Player.Instance.speedPlus = 1;
        DialogueUIManager.Instance.toHide();

        EndD_Script();
    }

    private void StartD_Script()
    {
        if (thisDialogue.name == "Тимлид Вадим")
        {
            WorldController.Instance.SpawnMitrich();
            AudioController.Instance.LaunchVadimScene();
        }
        else if (thisDialogue.name == "Дед Митрич")
            AudioController.Instance.LaunchMitrichScene();
    }

    private void EndD_Script()
    {
        if (thisDialogue.name == "Тимлид Вадим" || thisDialogue.name == "Дед Митрич")
        {
            AudioController.Instance.LaunchStreetMusic();
        }
        else if (thisDialogue.name == "Дед Митрич")
        {
            if (!Player.Instance.canGoToFinal)
                AudioController.Instance.LaunchStreetMusic();
            else
                WorldController.Instance.GoToFinalScene();
        }
    }
}
