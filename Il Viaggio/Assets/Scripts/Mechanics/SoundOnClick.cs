using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnClick : MonoBehaviour {

    private Pointable pointable;
    public AudioSource audioSource;

    void Start()
    {
        // evento azione
        GetComponent<Pointable>().ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            // attivazione audio
            audioSource.Play();
        });
    }
}

