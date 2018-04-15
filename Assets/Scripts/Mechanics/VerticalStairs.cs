using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalStairs : MonoBehaviour {

    public bool invertLockDirection = false;

    // componenti
    private GameObject player;
    private CapsuleCollider playerCollider;
    private PlayerController playerController;

    // supporto
    private RaycastHit rayHit;
    private Vector3 lockPosition;

    // Use this for initialization
    void Start ()
    {
        // player
        player = GameObject.FindGameObjectWithTag("player");
        playerController = player.GetComponent<PlayerController>();
        playerCollider = player.GetComponent<CapsuleCollider>();

        // posizione di discesa/salita
        if(invertLockDirection)
            lockPosition = transform.TransformPoint(GetComponent<BoxCollider>().center + (playerCollider.radius + 0.4f) * - Vector3.back);
        else
            lockPosition = transform.TransformPoint(GetComponent<BoxCollider>().center + (playerCollider.radius + 0.4f) * Vector3.back);
        
        // evento azione
        GetComponent<Pointable>().ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            playerController.ToggleClimbing(transform.gameObject);
            player.transform.position = new Vector3(lockPosition.x, player.transform.position.y + 0.015f, lockPosition.z);
        });
    }

    private void Update()
    {
        // verifica dei limiti su y della scalata
        if (playerController.isClimbing && playerController.ClimbedObject() == transform.gameObject)
        {
            // calcolo della base del giocatore
            Vector3 playerBase = player.transform.position - Vector3.up * (playerCollider.height / 2.0f);

            // direzione giocatore -> scala
            Vector3 dir = (transform.position - playerBase).normalized;
                    dir.y = 0.0f;

            // raycast giocatore -> scala
            if(!Physics.Raycast(playerBase, dir, out rayHit) ||
                rayHit.transform.gameObject != this.gameObject)
            {
                playerController.ToggleClimbing(transform.gameObject);
            }
            Debug.DrawRay(playerBase, dir, Color.magenta);
        }

        // posizione di lock
        Debug.DrawLine(new Vector3(lockPosition.x, player.transform.position.y + 0.015f, lockPosition.z), Vector3.up * 1000, Color.magenta);
    }
}
