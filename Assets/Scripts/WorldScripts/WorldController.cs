using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    [SerializeField]
    private GameObject street, home, finish, Mitrich, Dobrogneva_Final_TP, preCreditFinalScene, postCreditFinalScene;
    private Color transparentColor, opacityColor;


    private bool isFinalScene = false;

    public static bool IsFinalScene = false;
    public static bool IsBlackFinalScene = false;
    public static bool IsAfterCredits = false;

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
        IsFinalScene = true;
        toFinalScene();
    }

    private void toFinalScene()
    {
        home.SetActive(false);
        street.SetActive(false);
        finish.SetActive(true);
        AudioController.Instance.LaunchFinalMusic();
        Player.Instance.gameObject.transform.position = Dobrogneva_Final_TP.transform.position;
        //CameraController.Instance.toHideBlack();
    }

    public void GoToCreditScene()
    {
        home.SetActive(false);
        street.SetActive(false);
        finish.SetActive(false);
        CameraController.Instance.toCredits();
    }

    public void GoToPreCreditsFinalScene()
    {
        Player.Instance.GetComponent<SpriteRenderer>().color = transparentColor;
        home.SetActive(false);
        street.SetActive(false);
        finish.SetActive(false);
        IsBlackFinalScene = true;
        preCreditFinalScene.SetActive(true);
    }

    public void GoToPostCreditsFinalScene()
    {
        home.SetActive(false);
        street.SetActive(false);
        finish.SetActive(false);
        preCreditFinalScene.SetActive(false);
        IsAfterCredits = true;
        postCreditFinalScene.SetActive(true);
        Player.Instance.GetComponent<SpriteRenderer>().color = opacityColor;
    }

    public void GoToHome()
    {
        home.SetActive(true);
        street.SetActive(false);
    }

    private void Awake()
    {
        transparentColor = new Color(); opacityColor = new Color();
        transparentColor.a = 0; opacityColor.a = 1;
        transparentColor.r = 255; opacityColor.r = 255;
        transparentColor.g = 255; opacityColor.g = 255;
        transparentColor.b = 255; opacityColor.b = 255;


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

    }
}
