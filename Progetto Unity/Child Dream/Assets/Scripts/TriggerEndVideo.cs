using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEndVideo : MonoBehaviour {

    public GameObject gameManager;
    private GameManagerScript m_managerScript;

    private void Awake()
    {
        if (gameManager == null)
        {
            Debug.Log(this.name + "missing Video Player");
        }
        else
        {
            m_managerScript = gameManager.GetComponent<GameManagerScript>();
            if (m_managerScript == null)
            {
                Debug.Log(this.name + "missing Video Player component");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        DeathManager death = other.gameObject.GetComponent<DeathManager>();
        if (death != null)
        {
            m_managerScript.StartEndVideo();
        }

    }

}
