using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour {

    public bool audioInGame = false;

    public AudioMixerSnapshot VideoPlaying;
    public AudioMixerSnapshot Ingame;

    public GameObject MusicPlayer;


    private void Awake()
    {
        if (MusicPlayer == null)
        {
            Debug.Log(this.name + "Music player missing");
        }

        if (VideoPlaying == null)
        {
            Debug.Log(this.name + "VideoPlaying snapshot missing");
        }

        if (Ingame == null)
        {
            Debug.Log(this.name + "Ingame snapshot missing");
        }
    }


    public void TransictionToGame()
    {
        audioInGame = true;
        MusicPlayer.SetActive(true);
        Ingame.TransitionTo(0.1f);
        

    }

    public void TransictionToVideo()
    {
        audioInGame = false;
        MusicPlayer.SetActive(false);
        VideoPlaying.TransitionTo(0f);
    }
}
