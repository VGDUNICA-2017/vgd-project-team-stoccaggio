using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalStairs : MonoBehaviour {

    // variabili
    public float stepLength = 3.0f;

    // componenti
    private BoxCollider cl;
    private GameObject player;
    private CapsuleCollider playerCollider;
    private PlayerController playerController;

    // supporto
    private float heightLimit;
    private RaycastHit rayHit;
    private Vector3 lockPosition;

    // Use this for initialization
    void Start ()
    {
        // calcolo del limite della scala
        cl = GetComponent<BoxCollider>();
        heightLimit = cl.size.y + transform.position.y;

        // player
        player = GameObject.FindGameObjectWithTag("player");
        playerController = player.GetComponent<PlayerController>();
        playerCollider = player.GetComponent<CapsuleCollider>();

        // posizione di discesa/salita
        lockPosition = transform.TransformPoint(new Vector3(-0.5f, 0.0f, 0.0f));
        lockPosition = Vector3.MoveTowards(lockPosition, transform.forward, playerCollider.radius + 0.4f);

        // evento azione
        GetComponent<Pointable>().ActionHandler += new Pointable.ActionEventHandler(() =>
        {
            playerController.ToggleClimbing();
            player.transform.position = new Vector3(lockPosition.x, player.transform.position.y + 0.015f, lockPosition.z);
        });
    }

    private void Update()
    {
        Debug.DrawLine(new Vector3(lockPosition.x, player.transform.position.y + 0.015f, lockPosition.z), Vector3.up * 1000, Color.magenta);

        if (playerController.isClimbing)
        {
            // verifica dei limiti su y della scalata
            Vector3 dir = (transform.position - player.transform.position - Vector3.up * (playerCollider.height / 2.0f)).normalized;
                    dir.y = 0.0f;

            if(!Physics.Raycast(player.transform.position - Vector3.up * (playerCollider.height / 2.0f), dir, out rayHit) ||
                rayHit.transform.gameObject != this.gameObject)
            {
                playerController.ToggleClimbing();
            }

            Debug.DrawRay(player.transform.position - Vector3.up * (playerCollider.height / 2.0f), dir, Color.magenta);
        }
    }
}
