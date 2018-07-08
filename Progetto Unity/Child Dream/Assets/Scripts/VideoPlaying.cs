using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoPlaying : MonoBehaviour {
    VideoPlayer video;
    GameObject UI;

    bool hasPlayed = false;
	// Use this for initialization
	void Awake () {
        video = GetComponentInChildren<VideoPlayer>();
        if (video == null)
            Debug.Log(this.name + " no video found!");

        UI = this.transform.GetChild(1).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (!hasPlayed && Time.unscaledTime > 5f)
        {
            Time.timeScale = 0f;
            if (!video.isPlaying)
            {
                UI.SetActive(false);
                hasPlayed = true;
                StartCoroutine("StartTime");
                
            }
            else if (Input.GetButtonDown("Jump"))
            {
                video.Stop();
                UI.SetActive(false);
                hasPlayed = true;
                StartCoroutine("StartTime");
            }
        }
            
	}

    IEnumerator StartTime() {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
    }
}
