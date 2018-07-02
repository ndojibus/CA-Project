using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathObject : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        DeathManager death = other.gameObject.GetComponent<DeathManager>();
        if (death != null)
        {
            death.Dead = true;
        }
    }
}
