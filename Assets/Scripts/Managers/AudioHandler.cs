using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] _music; // 0 = menu, 1 = day, 2 = night

    private AudioSource _musicPlayer;

    public void PlayTheme(int index)
    {
        _musicPlayer.clip = _music[index];
        _musicPlayer?.Play();
    }
}
