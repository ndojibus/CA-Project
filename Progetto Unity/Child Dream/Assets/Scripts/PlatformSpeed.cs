using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpeed : MonoBehaviour {

    [Range(0.0f, 2f)]
    public float platformSpeed = 1f;

    Animator platformAnimator;

	// Use this for initialization
	void Awake () {
        platformAnimator = this.GetComponent<Animator>();
        if (platformAnimator == null)
            Debug.LogError(this.name + " has no animator attacched!");
        else
            platformAnimator.SetFloat("Speed", platformSpeed);

    }
}
