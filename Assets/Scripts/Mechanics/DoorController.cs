using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    public bool isOpen;
    public bool isLocked;

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
        SetLock(isLocked);

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            if (!isLocked)
            {
                controller(isOpen);
                isOpen = !isOpen;
            }
            else
            {
                pointable.pointedText = "Bloccata";
            }
        });
    }

    private void controller(bool isActive)
    {
        // animazione
        if (!isActive)
        {
            animator.SetTrigger("Open");
            pointable.pointedText = "[F] Chiudi";
            pointable.RefreshText();
        }
        else
        {
            animator.SetTrigger("Close");
            pointable.pointedText = "[F] Apri";
            pointable.RefreshText();
        }

        // audio apertura/chiusura
        audioSource.Play();
    }

    public void SetLock(bool locked)
    {
        isLocked = locked;

        if (isLocked)
            pointable.pointedText = "Bloccata";
        else if (isOpen)
            pointable.pointedText = "[F] Chiudi";
        else
            pointable.pointedText = "[F] Apri";
    }
}
