using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBarkingTrigger : MonoBehaviour {

    private AudioSource barkSound;
    private bool hasPlayed = false;

    private void Awake()
    {
        barkSound = this.GetComponent<AudioSource>();
        if (barkSound == null)
            Debug.Log(this.name + "Bark Audio Source missing component\n");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player" && !hasPlayed)
        {
            barkSound.PlayOneShot(barkSound.clip);
            hasPlayed = true;
            
        }
    }
}
