using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTimerStarter : MonoBehaviour {

    private bool started = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!started)
        {
            started = true;
            SceneController.CurrentGameObject.GetComponent<Terra>().RocketLaunch1();
        }
    }
}
