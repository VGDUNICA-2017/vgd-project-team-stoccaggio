using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCentrifuga : MonoBehaviour {

    public bool T_ActivatedOpen = true;
    public bool T_ActivatedClose = false;
    public bool activateTrigger = false;

    public GameObject centrifuga;

    public GameObject textO;
    public GameObject textC;

    bool active = false;

    void Start()
    {
        textC.SetActive(false);
        textO.SetActive(false);
        T_ActivatedOpen = true;
        T_ActivatedClose = false;

        active = false;
        
    }

    void Update()
    {
        if (T_ActivatedOpen == true)
            T_ActivatedClose = false;

        else if (T_ActivatedClose == true)
            T_ActivatedOpen = false;

        if (Input.GetKeyDown(KeyCode.F) && activateTrigger == true)
        {

            if (T_ActivatedOpen) // Se l'impianto è spento
            {
                T_ActivatedOpen = false;
                T_ActivatedClose = true;
                textO.SetActive(false);
                textC.SetActive(true);

                active = true;
                Controller(active);
            }
            else if (T_ActivatedClose) //Se l'impianto è acceso
            {
                T_ActivatedOpen = true;
                T_ActivatedClose = false;
                textO.SetActive(true);
                textC.SetActive(false);

                active = false;
                Controller(active);

            }
        }
    }

    void OnTriggerEnter(Collider col) //Se entri nel raggio dell'impianto
    {
        if (col.gameObject.tag == "player")
        {

            activateTrigger = true;
            if ((T_ActivatedOpen == true))
                textO.SetActive(true);

            if ((T_ActivatedClose == true))
                textC.SetActive(true);
        }

    }

    void OnTriggerExit(Collider col) //Se esci dal raggio dell'impianto
    {
        if (col.gameObject.tag == "player")
        {
            textO.SetActive(false);
            textC.SetActive(false);
            activateTrigger = false;
        }

    }

    void Controller(bool value) //Setta l'animazione delle particelle a seconda che l'impianto sia acceso o spento
    {
        if (value)
        {
            centrifuga.gameObject.GetComponent<Animator>().StartPlayback();
        }

        else
        {
            centrifuga.gameObject.GetComponent<Animator>().StopPlayback();
        }

    }
}
