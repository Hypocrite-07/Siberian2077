using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    [SerializeField]
    private GameObject street, home, finish;
    public void GoToStreet()
    {
        street.SetActive(true);
        home.SetActive(false);
        FindObjectOfType<AudioController>().LaunchStreetMusic();
    }

    public void GoToHome()
    {
        home.SetActive(true);
        street.SetActive(false);
    }

    private void Awake()
    {

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
