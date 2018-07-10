using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoPlaying : MonoBehaviour {

    VideoPlayer video;

    public GameObject m_videoPlayer;
    public GameObject m_blackBlackground;

    public bool hasPlayed = false;

    //fare controllo
	// Use this for initialization
	void Awake () {
        video = m_videoPlayer.GetComponent<VideoPlayer>();
        if (video == null)
            Debug.Log(this.name + " no video found!");

        
        if (m_blackBlackground== null)
        {
            Debug.Log(this.name + "missing Black Background");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!hasPlayed)
        {
            Time.timeScale = 0f;
            if (!video.isPlaying)
            {
                m_blackBlackground.SetActive(false);
                hasPlayed = true;
                Time.timeScale = 1f;

            }
            else if (Input.GetButtonDown("Jump"))
            {
                video.Stop();
                m_blackBlackground.SetActive(false);
                hasPlayed = true;
                Time.timeScale = 1f;
            }
        }
            
	}


}
