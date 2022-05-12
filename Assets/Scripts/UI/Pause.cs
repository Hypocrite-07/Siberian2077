using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    /*
    [SerializeField] private Button AudioButton;
    [SerializeField] private Sprite OnMusic;
    [SerializeField] private Sprite OffMusic;
    [SerializeField] private AudioSource mainAudioSource;
    */

    public static Pause Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        pausePanel.SetActive(false);
    }

    public void SetPause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    void Start()
    {
        //AudioButton.GetComponent<Image>().sprite = OnMusic;
        //mainAudioSource.GetComponent<AudioSettings>().AudioPlay = true;
    }

    public void ToMenu()
    {
        UIHandler.Instance.toMenu();
        Time.timeScale = 1;
    }

    /*
    public void RestartLevel()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex));
        Time.timeScale = 1;
    }
    */
    public void PauseOff()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    

}
