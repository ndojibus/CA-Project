using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathObject : MonoBehaviour {

    private AudioSource deathSound;

    private void Awake()
    {
        deathSound = this.GetComponent<AudioSource>();
        if (deathSound == null)
            Debug.Log(this.name + "Death Audio Source missing component\n");
    }

    private void OnTriggerEnter(Collider other)
    {
        DeathManager death = other.gameObject.GetComponent<DeathManager>();
        if (death != null)
        {
            deathSound.PlayOneShot(deathSound.clip);
            death.Dead = true;
        }
    }
}
