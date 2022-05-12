using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDialogue : MonoBehaviour
{
    public bool isTalked;
    public GameObject ButtonAdvice;
    private bool nearTheNPC = false;
    public Dialogue dialogue;

    void Update()
    {
        if (nearTheNPC && Input.GetKeyDown(KeyCode.E))
        {
            //FindObjectOfType<AudioController>().LaunchPetuhi();
            QuestDialogue qd = GetComponent<QuestDialogue>();
            qd.TriggerDialogue();
        }
    }

    void TriggerDialogue()
    {
        if (!isTalked || dialogue.isLoop)
        {
            if (dialogue.isReadable)
            {
                if (dialogue.isLoop)
                {
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                }
            }
            else
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
            dialogue.isReadable = true;
            isTalked = true;
            enabled = false;
        }
    }

    private void FixedUpdate()
    {
        /*
        if (nearTheNPC)
        {
            ButtonAdvice.gameObject.SetActive(true);
        }
        else
        {
            ButtonAdvice.gameObject.SetActive(false);
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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
    private void SetNearTheNPC(bool value)
    {
        nearTheNPC = value;
    }
}
