using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameManagerScript : MonoBehaviour {


    private bool isStart = true;
    private bool isPlaying = false;
    private bool isEnd = false;
    

    public GameObject m_blackRenderer;

    private MusicController m_musicController;
    
    

    public GameObject VideoPlayer;
    public GameObject EndVideoPlayer;

    private VideoPlayer m_videoPlayerComp;
    private VideoPlayer m_endVideoPlayerComp;
    
    




    private void Awake()
    {
        m_musicController = this.GetComponent<MusicController>();
        if (m_musicController == null)
        {
            Debug.Log(this.name + "missing Music Controller");
        }

        if(VideoPlayer == null)
        {
            Debug.Log(this.name + "missing Video Player");
        }
        else
        {
            m_videoPlayerComp = VideoPlayer.GetComponent<VideoPlayer>();
            if (m_videoPlayerComp == null)
            {
                Debug.Log(this.name + "missing Video Player component");
            }
        }

        if (EndVideoPlayer == null)
        {
            Debug.Log(this.name + "missing End Video Player");
        }
        else
        {
            m_endVideoPlayerComp = VideoPlayer.GetComponent<VideoPlayer>();
            if (m_videoPlayerComp == null)
            {
                Debug.Log(this.name + "missing Video Player component");
            }
        }

        if (m_blackRenderer == null)
        {
            Debug.Log(this.name + "missing Black Renderer");
        }

    }

    private void Update()
    {

        if (isStart)
        {
            StartInitialVideo();
            isStart = false;
        }


        if (isPlaying==true && Input.GetButtonDown("Jump"))
        {
            StopVideo();
        }

        if(isPlaying==true && (!m_videoPlayerComp.isPlaying || !m_endVideoPlayerComp.isPlaying))
        {
            StopVideo();
        }

    }

    private void StartInitialVideo()
    {
        m_musicController.TransictionToVideo();
        m_blackRenderer.SetActive(true);
        VideoPlayer.SetActive(true);
        isPlaying = true;
        Time.timeScale = 0f;


    }

    public void StartEndVideo()
    {
        m_musicController.TransictionToVideo();
        m_blackRenderer.SetActive(true);
        EndVideoPlayer.SetActive(true);
        m_endVideoPlayerComp.Play();
        
        Time.timeScale = 0f;

        isEnd = true;
        isPlaying = true;
    }

    private void StopVideo()     
    {
        EndVideoPlayer.SetActive(false);
        VideoPlayer.SetActive(false);
        m_blackRenderer.SetActive(false);
        isPlaying = false;
        Time.timeScale = 1f;
        m_musicController.TransictionToGame();

        if (isEnd)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        }
    }
}
