using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {


    private bool isStart = true;
    private bool isPlaying = false;
    
    private bool isPaused = true;
    private bool quitGame = false;
    

    public GameObject m_blackRenderer;

    private MusicController m_musicController;
    
    

    public GameObject VideoPlayer;
    public GameObject EndVideoPlayer;
    public GameObject PausePanel;

    public GameObject Player;
    private PlayerControl m_playerControl;

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

        if (Player == null)
        {
            Debug.Log(this.name + "missing Player");
        }
        else
        {
            m_playerControl = Player.GetComponent<PlayerControl>();
            if (m_playerControl == null)
            {
                Debug.Log(this.name + "missing Player control");
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

        if (Input.GetButtonDown("Cancel"))
        {
            PauseGame();
        }

    }

    public void PauseGame()
    {
        if (isPaused)
        {
            PausePanel.SetActive(false);
            m_playerControl.Controllable = true;
        }
        else
        {
            PausePanel.SetActive(true);
            m_playerControl.Controllable = false;
        }
        isPaused = !isPaused;
    }

    public void StartInitialVideo()
    {
        m_musicController.TransictionToVideo();
        m_blackRenderer.SetActive(true);
        VideoPlayer.SetActive(true);
        //m_videoPlayerComp.Play();
        isPlaying = true;
        Time.timeScale = 0f;
        PausePanel.SetActive(false);
        m_playerControl.Controllable = false;

    }

    public void StartEndVideo(bool quitGame= false)
    {
        this.quitGame = quitGame;
        Time.timeScale = 0f;
        m_musicController.TransictionToVideo();
        m_blackRenderer.SetActive(true);
        EndVideoPlayer.SetActive(true);
        m_endVideoPlayerComp.Play();
        
        
        PausePanel.SetActive(false);
        m_playerControl.Controllable = false;

        
        isPlaying = true;
    }

    private void StopVideo()     
    {
        m_videoPlayerComp.Stop();
        EndVideoPlayer.SetActive(false);
        VideoPlayer.SetActive(false);
        m_blackRenderer.SetActive(false);
        isPlaying = false;
        Time.timeScale = 1f;
        m_musicController.TransictionToGame();

   
        PausePanel.SetActive(true);
        m_playerControl.Controllable = false;

        //Se è il video finale
        if (quitGame)
        {
           
            QuitGame();
        }
        

        
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }
}
