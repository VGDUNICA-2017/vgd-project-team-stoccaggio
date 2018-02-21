﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorWithObject : MonoBehaviour {

    public bool T_ActivatedOpen = true;
    public bool T_ActivatedClose = false;
    public bool activateTrigger = false;

    public GameObject textO;
    public GameObject textC;

    public GameObject[] item;

    Animator animator;
    bool doorOpen;

    public GameObject doorOpenSound;
    public GameObject doorCloseSound;

    void Start()
    {
        textC.SetActive(false);
        textO.SetActive(false);
        T_ActivatedOpen = true;
        T_ActivatedClose = false;

        animator = GetComponent<Animator>();
        doorOpen = false;


        doorCloseSound.SetActive(false);
        doorOpenSound.SetActive(false);
    }

    void Update()
    {

        if (T_ActivatedOpen == true)
            T_ActivatedClose = false;

        else if (T_ActivatedClose == true)
            T_ActivatedOpen = false;

        if (Input.GetKeyDown(KeyCode.F) && activateTrigger == true)
        {
            if(IsBlocked())
            {
                if (T_ActivatedOpen) // Se la porta è chiusa
                {
                    T_ActivatedOpen = false;
                    T_ActivatedClose = true;
                    textO.SetActive(false);
                    textC.SetActive(true);
                    doorOpen = true;

                    doorOpenSound.SetActive(true);
                    doorCloseSound.SetActive(false);

                    if (doorOpen) //Se la porta è chiusa, la setta aperta e fa l'animazione per aprirla
                    {
                        doorOpen = true;
                        doorController("Open");
                    }

                }
                else if (T_ActivatedClose) //Se la porta è aperta
                {
                    T_ActivatedOpen = true;
                    T_ActivatedClose = false;
                    textO.SetActive(true);
                    textC.SetActive(false);

                    doorCloseSound.SetActive(true);
                    doorOpenSound.SetActive(false);

                    if (doorOpen) //Se la porta è aperta, la setta chiusa e fa l'animazione per chiuderla
                    {
                        doorOpen = false;
                        doorController("Close");
                    }
                }
            }
            else
            {
                print("Porta bloccata!");
            }
        }
    }

    void OnTriggerEnter(Collider col) //Se entri nel raggio della porta
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

    void OnTriggerExit(Collider col) //Se esci dal raggio della porta
    {
        if (col.gameObject.tag == "player")
        {
            textO.SetActive(false);
            textC.SetActive(false);
            activateTrigger = false;
        }

    }

    void doorController(string direction) //Setta l'animazione a seconda della "direzione"
    {
        animator.SetTrigger(direction);
    }

    private bool IsBlocked() //Restituisce flase se la porta è bloccata, ovvero il player non ha raccolto tutti gli item per sbloccarla
    {
        List<Item> app = SceneController.CurrentScene.GetItems();
        
        int count = 0;
        bool flag = true;
        while(count < item.Length)
        {
            if(!(app.Contains(new Item (item[count].GetComponent<Pointable>().pointedSubText, item[count].GetComponent<Pointable>().pointedSubText)))) //Se l'oggetto non è contenuto nella lista dell'inventario
            {
                flag = false; //Setto a false così da non permettere l'apertura della porta
            }
            count++;
        }

        return flag;
    }
}
