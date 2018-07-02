using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public float smoothing = 5f;

    Vector3 offset;
	// Use this for initialization
	void Awake () {
        if (target == null)
            Debug.LogError(this.name + ": Target don't found!");
    }

    void Start() {
        offset = this.transform.position - target.position;
    }
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 targetCameraPosition = target.position + offset;

        this.transform.position = Vector3.Lerp(this.transform.position, targetCameraPosition, smoothing * Time.deltaTime);
    }
}
