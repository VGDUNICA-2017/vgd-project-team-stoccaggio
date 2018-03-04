﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpiantoPiscina : MonoBehaviour {

    public GameObject npc;
    public GameObject vaporeon;

    private bool active;

    private Pointable pointable;

    void Start()
    {
        // componente pointable
        pointable = GetComponent<Pointable>();

        // inizializzazione
        active = false;
        pointable.pointedText = "[F] Usa impianto";
        pointable.pointedSubText = "aumenta la temperatura";
        vaporeon.GetComponent<ParticleSystem>().Stop();

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            active = !active;
            controller(active);
        });
    }

    private void controller(bool isActive)
    {
        if (isActive)
        {
            pointable.pointedSubText = "diminuisci la temperatura";
            pointable.RefreshText();
            vaporeon.GetComponent<ParticleSystem>().Play();
            npc.GetComponent<EnemyFieldofView>().maxDistanceView /= 2f;
        }
        else
        {
            pointable.pointedSubText = "aumenta la temperatura";
            pointable.RefreshText();
            vaporeon.GetComponent<ParticleSystem>().Stop();
            npc.GetComponent<EnemyFieldofView>().maxDistanceView *= 2f;
        }
    }
}
