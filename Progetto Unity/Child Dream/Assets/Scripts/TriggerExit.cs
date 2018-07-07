using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExit : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        DeathManager death = other.gameObject.GetComponent<DeathManager>();
        if (death != null)
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

    }
}
