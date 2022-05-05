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

    public event OnStartDialogue onStartDialogue;
    public event OnEndDialogue onEndDialogue;

    public delegate void OnStartDialogue(DialogueManager dm);
    public delegate void OnEndDialogue(DialogueManager dm);



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
        onStartDialogue?.Invoke(this);
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
        if (gameObject.name == "Teamlead")
            FindObjectOfType<AudioController>().LaunchVadimScene();
        if (FindObjectOfType<Player>().isDream)
            isDreamPlayerUpdate = true;
        FindObjectOfType<Player>().speedPlus = 0;
        FindObjectOfType<Player>().isTalk = true;
        if (messages.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        Message message = messages.Dequeue();
        if (message.isAuthor)
        {
            nameText.text = "";
        }
        if (finalDialogue != null)
        {
            if (finalDialogue.isFinal)
            {
                nameText.text = message.nameNpc;
                nameText.color = message.color;
            }
        }
        else
        {
            if (!message.isNPC)
            {
                nameText.text = Player.Instate.MainName;
                nameText.color = Player.Instate.ColorName;
            }
            else
            {
                nameText.text = _localNameText;
                nameText.color = Color.cyan;
            }
        }
        if (message.notificationText.Length != 0)
        {
            FindObjectOfType<DialogueUIManager>().toShowNotification(message.notificationText);
        }
        dialogueText.text = message.messageText;
    }

    public void EndDialogue()
    {
        onEndDialogue?.Invoke(this);
        if (FindObjectOfType<Player>().isDream)
            FindObjectOfType<CameraController>().toHideBlack();
        FindObjectOfType<Player>().isTalk = false;
        FindObjectOfType<Player>().speedPlus = 1;
        FindObjectOfType<DialogueUIManager>().toHide();
    }
}
