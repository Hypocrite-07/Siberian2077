using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    public GameObject Petuhi_1, Street, VadimScene, MitrichScene, FinalMusic, Credits;

    private GameObject _lastMusicGameObject;

    public static AudioController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void LaunchPetuhi()
    {
        LaunchMusic(Petuhi_1);
        Debug.Log("MUS: PETUHI");
    }

    public void LaunchStreetMusic()
    {
        LaunchMusic(Street);
        Debug.Log("MUS: STREET");
    }

    public void LaunchVadimScene()
    {
        LaunchMusic(VadimScene);
        Debug.Log("MUS: VADIM");
    }

    public void LaunchMitrichScene()
    {
        LaunchMusic(MitrichScene);
        Debug.Log("MUS: MITRICH");
    }

    public void LaunchFinalMusic()
    {
        LaunchMusic(FinalMusic);
        Debug.Log("MUS: FINAL");
    }

    private void LaunchMusic(GameObject _gameObject)
    {
        if (_lastMusicGameObject == _gameObject && _lastMusicGameObject.GetComponent<AudioSource>().isPlaying)
            return;
        if (_lastMusicGameObject != null && _lastMusicGameObject.GetComponent<AudioSource>().isPlaying)
            _lastMusicGameObject.GetComponent<AudioSource>().Stop();
        _lastMusicGameObject = _gameObject;
        _gameObject.GetComponent<AudioSource>().Play();
    }

}
