using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldofView : MonoBehaviour
{
    // variabili
    public float angleVisual = 90.0f;
    public float maxDistanceView = 35.0f;

    // componenti
    private CapsuleCollider cl;

    // supporto
    private GameObject player;
    private CapsuleCollider playerCollider;
    private Vector3 sourceEye;
    private Vector3 playerHead;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerCollider = player.GetComponent<CapsuleCollider>();

        cl = GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate()
    {
        RaycastHit raycastBullet;

        // posizione occhi sorgente
        sourceEye = transform.position + Vector3.up * cl.height;

        // posizione testa bersaglio
        playerHead = player.transform.position + Vector3.up * (playerCollider.height / 2.0f);

        // raggio dall'occhio del personaggio alla testa suo bersaglio
        if (Physics.Raycast(sourceEye, playerHead - sourceEye, out raycastBullet, maxDistanceView))
        {
            // controlla se il raycast colpisce direttamente il bersaglio
            if (raycastBullet.collider == playerCollider)
            {
                // controlla l'angolo
                if (Vector3.Angle(transform.forward, playerHead - sourceEye) < angleVisual / 2.0f)
                {
                    print("CASSAUUUU");
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(sourceEye, maxDistanceView);
        Gizmos.DrawRay(sourceEye, playerHead - sourceEye);
    }
}