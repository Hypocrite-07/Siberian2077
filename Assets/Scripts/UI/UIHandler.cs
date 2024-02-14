using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    public Sprite spriteAuthors, spriteAuthorsInMouse;

    public static UIHandler Instance { get; private set; }

    public void toWeb(string url)
    {
        Application.OpenURL(url);
    }

    public void toAbout()
    {
        AsyncLoading(2);
    }

    public void toMenu()
    {
        WorldController.IsAfterCredits = false;
        WorldController.IsFinalScene = false;
        WorldController.IsBlackFinalScene = false;
        AsyncLoading(0);
    }

    public void toDiscord()
    {
        Application.OpenURL("https://discord.gg/X7FXSHd8Wy");
    }

    public void toPlay()
    {
        AsyncLoading(1);
    }

    private void AsyncLoading(int level)
    {
        StartCoroutine(waitScene(level));
    }

    IEnumerator waitScene(int level)
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(level);
        while (!loadScene.isDone)
        {
            yield return null;
        }
    }

    public void Exit()
    {
        Environment.Exit(0);
    }

    public void Awake()
    {
        Instance = this;
    }
}
