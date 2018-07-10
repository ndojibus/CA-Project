using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class EndVideoPlaying : MonoBehaviour
{
    VideoPlayer video;
 

    
    public bool hasPlayed = false;
    // Use this for initialization
    void Awake()
    {
        video = GetComponentInChildren<VideoPlayer>();
        if (video == null)
            Debug.Log(this.name + " no video found!");

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasPlayed)
        {
            Time.timeScale = 0f;
            if (!video.isPlaying)
            {
                
                hasPlayed = true;
                Time.timeScale = 1f;

            }
            else if (Input.GetButtonDown("Jump"))
            {
                video.Stop();
                
                hasPlayed = true;
                Time.timeScale = 1f;
            }
        }

    }


}