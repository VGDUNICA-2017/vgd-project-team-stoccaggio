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
        }
    }
}
