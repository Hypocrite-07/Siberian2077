using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    [SerializeField]
    private GameObject street, home, finish, Mitrich;

    private bool isFinalScene = false;

    public static WorldController Instance { get; private set; }

    public void GoToStreet()
    {
        street.SetActive(true);
        home.SetActive(false);
        AudioController.Instance.LaunchStreetMusic();
    }

    public void SpawnMitrich()
    {
        Mitrich.SetActive(true);
    }

    public void GoToFinalScene()
    {
        //CameraController.Instance.toStartFinalBlack();
        isFinalScene = true;
    }

    private void toFinalScene()
    {
        home.SetActive(false);
        street.SetActive(false);
        finish.SetActive(true);
        AudioController.Instance.LaunchFinalMusic();
        CameraController.Instance.toHideBlack();
    }

    public void GoToHome()
    {
        home.SetActive(true);
        street.SetActive(false);
    }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isFinalScene)
        {
            toFinalScene();
        }
    }
}
