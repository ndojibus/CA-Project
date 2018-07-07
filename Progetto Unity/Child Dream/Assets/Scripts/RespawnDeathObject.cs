using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnDeathObject : MonoBehaviour {
    public float returnTime = 0.1f;

    Queue<Rigidbody> spikeBodyQueue ;

    private void Awake()
    {
        spikeBodyQueue = new Queue<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        DeathObject spikeFruit = other.gameObject.GetComponent<DeathObject>();
        if (spikeFruit != null)
        {
            Rigidbody spikeBody = other.gameObject.GetComponent<Rigidbody>();
            if (spikeBody != null && ( spikeBodyQueue.Count == 0 || spikeBody != spikeBodyQueue.Peek()))
            {   
                spikeBodyQueue.Enqueue(spikeBody);
                spikeBodyQueue.Peek().isKinematic = true;
                Invoke("Fall", returnTime);
            }
        }

    }

    void Fall() {
        spikeBodyQueue.Peek().transform.position = this.transform.position;
        spikeBodyQueue.Peek().isKinematic = false;
        spikeBodyQueue.Dequeue();
    }

}
