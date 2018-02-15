using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldofFeel : MonoBehaviour
{
    public float baseDistance = 3.0f;
    public float minDistance = 1.5f;

    private GameObject player;
    private PlayerController playerController;
    private Checkpoints playerCheckpoint;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerController = player.GetComponent<PlayerController>();
        playerCheckpoint = player.GetComponent<Checkpoints>();
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < baseDistance * playerController.soundProduced || distance <= minDistance)
        {
            print("CASSATO VICINO!");
            
            player.transform.position = playerCheckpoint.lastCheckpoint.transform.position;
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, playerController.isIdle ? minDistance : baseDistance * playerController.soundProduced);
    }
}
