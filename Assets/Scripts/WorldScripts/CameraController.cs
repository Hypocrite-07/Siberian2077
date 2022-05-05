using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Canvas canvas;
    [SerializeField] public GameObject homeLocation;


    private Vector3 pos;

    public static CameraController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        if (!player)
            player = FindObjectOfType<Player>().transform;
    }

    public void toHideBlack()
    {
        if (canvas.enabled)
        {
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
