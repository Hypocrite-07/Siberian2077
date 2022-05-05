using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    public string Name;
    public bool isFind;
    public GameObject ButtonAdvice;
    private bool nearTheItem = false;

    private void Start()
    {
        ButtonAdvice = gameObject.transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {
        if (gameObject.name == "Telephone")
        {
            return;
        }
        if (ButtonAdvice != null)
        {
            if (nearTheItem)
            {
                ButtonAdvice.SetActive(true);
            }
            else
            {
                ButtonAdvice.SetActive(false);
            }
        }
        
    }

        void Update()
        {
        /*
        if (gameObject.name == "Telephone")
        {
            isFind = true;
        }
        */
        if (nearTheItem && Input.GetKeyDown(KeyCode.E))
        {
            DialogueUIManager.ToShowNotification($"�������������� ���������.");
            if (gameObject.name == "Boots")
                Player.Instance.Redisign();
            if (gameObject.name == "Goose")
            {
                if (Player.Instance.isHasOtvertka)
                {
                    GameObject.Find("Goose_Invis_Has").GetComponent<DialogueTrigger>().TriggerDialogue();
                }
                else
                {
                    GameObject.Find("Goose_Invis_NoHas").GetComponent<DialogueTrigger>().TriggerDialogue();
                }
            }
            if (gameObject.name == "SnowBugorok")
            {
                GameObject.Find("Invis_Dialogue_Snow_True").GetComponent<DialogueTrigger>().TriggerDialogue();
                Player.Instance.isHasOtvertka = true;
            }
            else if (gameObject.name == "SnowBugorok_0")
            {
                GameObject.Find("Invis_Dialogue_Snow_2").GetComponent<DialogueTrigger>().TriggerDialogue();
            }
            else if (gameObject.name == "SnowBugorok_1")
            {
                GameObject.Find("Invis_Dialogue_Snow_3").GetComponent<DialogueTrigger>().TriggerDialogue();
            }
            isFind = true;
            gameObject.SetActive(false);
        }
        }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SetNearTheItem(true);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SetNearTheItem(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SetNearTheItem(false);
    }

    private void SetNearTheItem(bool value)
    {
        nearTheItem = value;
    }

}
