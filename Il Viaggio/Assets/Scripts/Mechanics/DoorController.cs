using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    public bool isOpen;
    public bool isLocked;
    public string hints;

    private Pointable pointable;
    private Animator animator;
    private AudioSource audioSource;
    private BoxCollider boxCollider;
    

    void Start()
    {
        // componenti
        pointable = GetComponent<Pointable>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();

        // inizializzazione
        SetLock(isLocked);

        // evento azione
        pointable.ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            SetLock(isLocked);
            pointable.RefreshText();
            if (!isLocked)
            {
                controller(isOpen);
                isOpen = !isOpen;
            }
            else
            {
                if(hints != null && hints.Trim() != "")
                    SceneController.CurrentScene.SpeakToSelf(hints);
                pointable.pointedText = "[F] Sblocca porta";
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
        StartCoroutine(DisableCollider());
        // audio apertura/chiusura
        audioSource.Play();
    }

    public void SetLock(bool locked)
    {
        isLocked = locked;

        if (isLocked)
            pointable.pointedText = "Porta bloccata";
        else if (isOpen)
            pointable.pointedText = "[F] Chiudi";
        else
            pointable.pointedText = "[F] Apri";
    }

    public void ForceLock()
    {
        // chiude la porta se è aperta
        if (isOpen)
        {
            controller(isOpen);
            isOpen = false;
        }

        // blocca la porta
        SetLock(true);
    }

    private IEnumerator DisableCollider()
    {
        boxCollider.enabled = false;
        yield return new WaitForSeconds(1);
        boxCollider.enabled = true;
    }
}
