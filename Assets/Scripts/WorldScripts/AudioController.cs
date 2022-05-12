using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    public GameObject Petuhi_1, HomeMusic, Street, VadimScene, MitrichScene, FinalMusic, Credits, crickets;

    private GameObject _lastMusicGameObject;

    public static AudioController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void LaunchPetuhi()
    {
        StartCoroutine(LaunchHomeMusic());
    }

    IEnumerator LaunchHomeMusic()
    {
        LaunchMusic(Petuhi_1, false);
        Debug.Log("MUS: PETUHI");
        while (Petuhi_1.GetComponent<AudioSource>().isPlaying)
        {
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(0f);
        LaunchMusic(HomeMusic, true);
        Debug.Log("MUS: HOME");
    }

    public void LaunchStreetMusic()
    {
        LaunchMusic(Street,true);
        Debug.Log("MUS: STREET");
    }

    public void LaunchVadimScene()
    {
        LaunchMusic(VadimScene, true);
        Debug.Log("MUS: VADIM");
    }

    public void LaunchMitrichScene()
    {
        LaunchMusic(MitrichScene, true);
        Debug.Log("MUS: MITRICH");
    }

    public void LaunchFinalMusic()
    {
        LaunchMusic(FinalMusic, true);
        Debug.Log("MUS: FINAL");
    }

    public void LaunchCricket()
    {
        LaunchMusic(crickets, true);
        Debug.Log("MUS: CRICKET");
    }



    private void LaunchMusic(GameObject _gameObject, bool _loop)
    {
        if (_lastMusicGameObject == _gameObject && _lastMusicGameObject.GetComponent<AudioSource>().isPlaying)
            return;
        if (_lastMusicGameObject != null && _lastMusicGameObject.GetComponent<AudioSource>().isPlaying)
            _lastMusicGameObject.GetComponent<AudioSource>().Stop();
        _lastMusicGameObject = _gameObject;
        _gameObject.GetComponent<AudioSource>().loop = _loop;
        _gameObject.GetComponent<AudioSource>().Play();
    }

}
