using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour {

    public bool isEndVideo = false;
    public bool isPlaying = false;

    public GameObject VideoPlayer;
    public GameObject EndVideoPlayer;

    private VideoPlayer m_video_start;
    private VideoPlayer m_video_end;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartInitialVideo()
    {

    }
}
