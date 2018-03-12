using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour {

    public GameObject lastCheckpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "checkpoint" && other.gameObject != lastCheckpoint)
        {
            lastCheckpoint.SetActive(false);
            lastCheckpoint = other.gameObject;
            Transform tr = other.gameObject.transform;
            PlayerPrefs.SetString("Scena", "Terra1");
            PlayerPrefs.SetFloat("x", tr.position.x);
            PlayerPrefs.SetFloat("y", tr.position.y);
            PlayerPrefs.SetFloat("z", tr.position.z);
        }
    }
}
