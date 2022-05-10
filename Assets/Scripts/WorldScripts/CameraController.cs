using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Canvas creditsCanvas;
    [SerializeField] public GameObject homeLocation;


    private Vector3 pos;

    public static CameraController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        if (!player)
            player = FindObjectOfType<Player>().transform;
    }

    public void toStartBlack(bool final)
    {
        if (final)
            StartCoroutine(showText("Финал.", 3, true));
        else
        {
            if (canvas.enabled)
                StartCoroutine(showText("Нажмите \"F\" чтобы проснуться", 5, false));
            else
            {
                canvas.enabled = true;
                toStartBlack(false);
            }
        }
    }

    IEnumerator showText(string text, int duration, bool final)
    {
        if (final)
            canvas.enabled = true;
        canvas.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>().enabled = true;
        while (duration >= 0)
        {
            canvas.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>().text = text;
            duration--;
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
        if (final)
            canvas.enabled = false;

    }

    public void toCredits()
    {
        creditsCanvas.transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(showCredits(5));
    }

    IEnumerator showCredits(int duration)
    {
        Debug.LogWarning("CreditShow");
        while (duration >= 0)
        {
            duration--;
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(0f);
        WorldController.Instance.GoToPostCreditsFinalScene();
        creditsCanvas.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void toHideBlack()
    {
        if (canvas.enabled)
        {
            if(!Player.Instance.canGoToFinal)
                DialogueUIManager.ToShowNotification("Задание выполнено.");
            canvas.enabled = false;
        }
    }

    public void toHideText()
    {
        if (canvas.enabled)
        {
            canvas.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>().enabled = false;
        }
    }

    private void Update()
    {
        if (!homeLocation.active)
        {
            try
            {
                pos = player.position;
                if (WorldController.IsFinalScene)
                    pos.x -= 2f;
                pos.z = -5f;
                pos.y += 2.17f;

                transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
            }
            catch (MissingReferenceException e)
            {
                pos.x = 0;
                pos.z = -20f;
                pos.y = 0f;

                transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
            }
        }
    }
}
