using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldofFeel : MonoBehaviour
{
    // variabili
    public string npcName;
    public float baseDistance = 6f;
    public float minDistance = 3f;

    private GameObject player;
    private PlayerController playerController;

    // allarme
    private float alertTimeout;
    private bool playerSeen;
    private bool playerCaught;
    public float alertTime = 4.0f;
    public GameObject allarmPoint;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerController = player.GetComponent<PlayerController>();

        playerSeen = false;
        playerCaught = false;
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < baseDistance * playerController.soundProduced || distance <= minDistance)
        {
            if (!playerSeen)
            {
                // avvistato per la prima volta
                playerSeen = true;
                alertTimeout = Time.time + alertTime;
                allarmPoint.gameObject.SetActive(true);
                SceneController.CurrentScene.NpcSpeak(npcName, "Mi è sembrato di sentire qualcosa...");
            }
            else
            {
                // il player è stato già avvistato
                if (playerSeen && !playerCaught)
                {
                    // controlla che non sia stato beccato
                    if (Time.time >= alertTimeout)
                    {
                        // beccato!
                        playerCaught = true;
                        SceneController.CurrentScene.NpcSpeak(npcName, "Allarme! C'e' un intruso! Chiamate le guardie!");
                        StartCoroutine(catchPlayer());
                    }
                }
            }
        }
        else
        {
            // player non più sotto la visuale dell'NPC
            if (playerSeen)
            {
                // il player era stato avvistato
                playerSeen = false;
                allarmPoint.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator catchPlayer()
    {
        yield return new WaitForSecondsRealtime(4);

        SceneController.CurrentScene.GameOver();
    }

    void OnDrawGizmosSelected()
    {
        /*Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, playerController.isIdle ? minDistance : baseDistance * playerController.soundProduced);*/
    }
}
