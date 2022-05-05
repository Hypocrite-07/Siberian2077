using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVadim : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            FindObjectOfType<AudioController>().LaunchVadimScene();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
