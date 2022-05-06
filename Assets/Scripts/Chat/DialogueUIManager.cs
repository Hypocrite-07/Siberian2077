using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUIManager : MonoBehaviour
{
    [SerializeField]
    public GameObject _dialogueCanvas, _dialogueSpeakerName, _dialogueSpeakerText, _dialogueHolderSpeakerName, _choosePanel, _notificationPanel, _notificationText;

    public static DialogueUIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public static void ToShowNotification(string text)
    {
        Instance.toShowNotification(text);
    }

    public void toShow()
    {
        _dialogueCanvas.SetActive(true);
    }

    public void toHide()
    {
        _dialogueCanvas.SetActive(false);
    }


    private void toShowNotification(string text)
    {
        StopAllCoroutines();
        //StopCoroutine(showNotification(text, 3));
        StartCoroutine(showNotification(text, 3));
    }

    IEnumerator showNotification(string text, int duration)
    {
        _notificationPanel.SetActive(true);
        while (duration >= 0)
        {
            _notificationText.GetComponent<Text>().text = text;
            duration--;
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
        _notificationPanel.SetActive(false);

    }
}
