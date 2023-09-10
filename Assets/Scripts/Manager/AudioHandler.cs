using System;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private StringEventChannel _musicEvent;
    [SerializeField] private AudioData[] _music;

    private AudioSource _musicPlayer;

    #region SETUP

    void OnEnable()
    {
        _musicEvent.OnStringEventRaised += PlayTheme;
    }

    void OnDisable()
    {
        _musicEvent.OnStringEventRaised -= PlayTheme;
    }

    private void Start()
    {
        _musicPlayer = gameObject.GetComponent<AudioSource>();

        PlayTheme("Menu");
    }
    
    #endregion

    public void PlayTheme(string name)
    {
        var theme = Array.Find<AudioData>(_music, audio => audio.Name == name);
        _musicPlayer.clip = theme.Clip;
        _musicPlayer.volume = theme.Volume;
        _musicPlayer.pitch = theme.Pitch;
        _musicPlayer.loop = theme.Loop;
        _musicPlayer?.Play();
    }
}
