﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower : MonoBehaviour {

    public GameObject water;
    public bool isActive;
    public GameObject audioSource;

    private Pointable pointable;

    void Start()
    {
        // componente pointable
        pointable = GetComponent<Pointable>();

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            isActive = !isActive;
            water.SetActive(isActive);

            // attivazione/disattivazione suono
            if(isActive)
                audioSource.GetComponent<AudioSource>().Play();
            else
                audioSource.GetComponent<AudioSource>().Stop();

        });
    }
}
