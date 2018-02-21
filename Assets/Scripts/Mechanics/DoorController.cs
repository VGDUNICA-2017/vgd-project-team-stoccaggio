using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    public bool isOpen;

    private Pointable pointable;
    private Animator animator;
    private AudioSource audioSource;

    void Start()
    {
        // componenti
        pointable = GetComponent<Pointable>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        // inizializzazione
        if (isOpen)
            pointable.pointedText = "[F] Chiudi";
        else
            pointable.pointedText = "[F] Apri";

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            controller(isOpen, true);
            isOpen = !isOpen;
        });
    }

    private void controller(bool isActive, bool withAudio)
    {
        // animazione
        if (!isActive)
        {
            animator.SetTrigger("Open");
            pointable.pointedText = "[F] Chiudi";
        }
        else
        {
            animator.SetTrigger("Close");
            pointable.pointedText = "[F] Apri";
        }

        // audio apertura/chiusura
        if(withAudio)
            audioSource.Play();
    }
}
