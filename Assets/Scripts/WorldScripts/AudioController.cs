using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    public GameObject Petuhi_1, Street, VadimScene, Credits;

    public void LaunchPetuhi()
    {
        StopPetuhi();
        LaunchPetuhi_1();
        StartCoroutine(AudioCoroutinePetuhi());
    }

    public void LaunchStreetMusic()
    {
        if (!VadimScene.GetComponent<AudioSource>().isPlaying)
            Street.GetComponent<AudioSource>().Play();
    }

    public void LaunchVadimScene()
    {
        if (Street.GetComponent<AudioSource>().isPlaying)
            Street.GetComponent<AudioSource>().Stop();
        if (!VadimScene.GetComponent<AudioSource>().isPlaying)
            VadimScene.GetComponent<AudioSource>().Play();
    }

    private IEnumerator AudioCoroutinePetuhi()
    {
        while (Petuhi_1.GetComponent<AudioSource>().isPlaying)
        {
            yield return null;
        }
        yield return null;

    }

    private void LaunchPetuhi_1()
    {
        Petuhi_1.GetComponent<AudioSource>().loop = false;
        Petuhi_1.GetComponent<AudioSource>().Play();
    }

    public void StopPetuhi()
    {
        Petuhi_1.GetComponent<AudioSource>().Stop();
    }
}
