using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TriggerExit : MonoBehaviour {
    public GameObject blackRenderer;

    VideoPlayer video;
    bool videoStarted = false;

    private void Awake()
    {
        if (blackRenderer == null)
            Debug.Log(this.name + " missing blackrenderer");

        video = GetComponent<VideoPlayer>();
        if (video == null)
            Debug.Log(this.name + " missing video");
    }

    private void OnTriggerEnter(Collider other)
    {
        DeathManager death = other.gameObject.GetComponent<DeathManager>();
        if (death != null)
        {
            videoStarted = true;
            Time.timeScale = 0f;
            blackRenderer.SetActive(true);
            video.Play();
        }

    }

    private void Update()
    {
        if (!video.isPlaying && videoStarted)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        }
    }
}
