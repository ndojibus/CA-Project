using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointObject : MonoBehaviour {
    private void OnTriggerStay(Collider other)
    {
        DeathManager death = other.gameObject.GetComponent<DeathManager>();
        if (death != null)
        {
            if (death.LastCheckpoint != this.transform)
                death.LastCheckpoint = this.transform;
        }
    }
}
