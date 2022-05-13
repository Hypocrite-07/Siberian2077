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
        if (!dialogue.isFinal)
        {
            nameText.text = dialogue.name;
            _localNameText = dialogue.name;
        }
        else
        {
            nameText.text = dialogue.messages[0].nameNpc;
            _localNameText = dialogue.messages[0].nameNpc;
        }
        FindObjectOfType<DialogueUIManager>().toShow();
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
                if (!thisDialogue.isFinal)
                {
                    nameText.text = _localNameText;
                    nameText.color = Color.cyan;
                }
                else
                {
                    Debug.LogWarning("Exect");
                    nameText.text = message.nameNpc;
                    nameText.color = message.color;
                }    
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
        if (Player.Instance.isDream)
        {
            Player.Instance.isDream = false;
            CameraController.Instance.toHideBlack();
        }
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
        if (thisDialogue.name == "Тимлид Вадим")
        {
            AudioController.Instance.LaunchStreetMusic();
        }
        else if (thisDialogue.name == "Дед Митрич")
        {
            Debug.LogWarning($"GoToFinal_2 {Player.Instance.canGoToFinal}");
            if (!Player.Instance.canGoToFinal)
                AudioController.Instance.LaunchStreetMusic();
            else
            {
                Debug.LogWarning("Final scene is running.");
                WorldController.Instance.GoToFinalScene();
            }
        }
        if (WorldController.IsAfterCredits)
        {
            UIHandler.Instance.toMenu();
        }
        else
        {
            if (WorldController.IsBlackFinalScene)
                WorldController.Instance.GoToCreditScene();
            else if (WorldController.IsFinalScene && thisDialogue.isFinal)
                WorldController.Instance.GoToPreCreditsFinalScene();
            else if (WorldController.IsFinalScene)
            {
                AudioController.Instance.LaunchFinalMusic();
                //Destroy(Player.Instance._rigidbody2D);
                WorldController.Instance.GoToFinalScene();
            }
        }
    }
}
