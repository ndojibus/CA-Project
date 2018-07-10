using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpeed : MonoBehaviour
{

    [Range(0.0f, 2f)]
    public float platformSpeed = 1f;

    Animator platformAnimator;
    AudioSource platformSound;

    bool ascending = false;
    bool descending = false;

    // Use this for initialization
    void Awake()
    {
        platformAnimator = this.GetComponent<Animator>();
        if (platformAnimator == null)
            Debug.LogError(this.name + " has no animator attacched!");
        else
            platformAnimator.SetFloat("Speed", platformSpeed);

        platformSound = this.GetComponent<AudioSource>();
        if (platformSound == null)
            Debug.Log(this.name + "Death Audio Source missing component\n");

    }

    private void Update()
    {   if (platformSound != null)
        {
            float animationTime = platformAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float currentTime = Mathf.Floor(animationTime);
            if ((animationTime > currentTime + 0.28f) && (animationTime < currentTime + 0.4) && !ascending)
            {
                if (platformSound.isPlaying)
                    platformSound.Stop();
                platformSound.PlayOneShot(platformSound.clip);
                Debug.Log("GOING");

                ascending = true;
                descending = false;
            }
            else if ((animationTime > currentTime + 0.78f) && (animationTime < currentTime + 0.9f) && !descending)
            {
                if (platformSound.isPlaying)
                    platformSound.Stop();
                platformSound.PlayOneShot(platformSound.clip);
                Debug.Log("RETURNING");

                descending = true;
                ascending = false;
            }
            //else
            //    Debug.Log("ANIMATION ANIMATION TIME: " + animationTime);
        }
    }
}
