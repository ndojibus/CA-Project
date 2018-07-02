using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour {

    public float respawnTime = 1f;

    Transform lastCheckpoint;
    public Transform LastCheckpoint { set { lastCheckpoint = value; } get { return lastCheckpoint; } }

    private bool dead = false;
    public bool Dead { set { dead = value; } }

    Rigidbody childBody;
    PlayerControl childControl;

    private void Awake()
    {
        childBody = GetComponent<Rigidbody>();
        if (childBody == null)
            Debug.LogError(this.name + ": Rigidbody don't found!");

        childControl = GetComponent<PlayerControl>();
        if (childBody == null)
            Debug.LogError(this.name + ": Control don't found!");
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (dead)
        {
            if (lastCheckpoint == null)
                Debug.LogError(this.name + " no last checkpoint set!");
            dead = false;
            StartCoroutine("Respawn", 0.2f);
            SetAll(false);
        }
	}

    IEnumerator Respawn() {
        this.transform.position = lastCheckpoint.position;
        yield return new WaitForSeconds(respawnTime);
        if (!childControl.FacingRight)
        {
            this.transform.Rotate(0, -180, 0);
            childControl.FacingRight = true;
        }
        SetAll(true);
    }

    void SetAll(bool set)
    {
        for (int i = 0; i < this.transform.childCount; i++) {
            this.transform.GetChild(i).gameObject.SetActive(set);
        }
        childControl.Controllable = set;

        childBody.isKinematic = !set;
    }
}
