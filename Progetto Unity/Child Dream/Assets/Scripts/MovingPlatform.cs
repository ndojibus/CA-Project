using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.tag == "Player")
        {
            collision.collider.transform.SetParent(this.transform);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.transform.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }

    }
}
