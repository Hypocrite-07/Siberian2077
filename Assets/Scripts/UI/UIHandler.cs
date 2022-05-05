using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    public Sprite spriteStartGame, spriteStartGameInMouse, spriteAuthors, spriteAuthorsInMouse;

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
}