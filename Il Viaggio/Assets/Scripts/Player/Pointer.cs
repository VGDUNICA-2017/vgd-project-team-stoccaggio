using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {

    // variabili
    public Camera playerCamera;
    public float maxDistance = 2.0f;
    public float startDistance = 0.3f;

    // supporto
    private RaycastHit rayHit;
    private GameObject pointedObject;
    private Pointable pointedScript;

    void Update()
    {
        // raycast dal centro della telecamera al resto del mondo
        Debug.DrawRay(playerCamera.transform.position + startDistance * playerCamera.transform.forward, playerCamera.transform.forward);
        if (Physics.Raycast(playerCamera.transform.position + startDistance * playerCamera.transform.forward, playerCamera.transform.forward, out rayHit, maxDistance))
        {
            // nuovo oggetto puntato
            if (pointedObject != rayHit.transform.gameObject)
            {
                // trigger del puntamento fuori dal vecchio oggetto
                if (pointedScript != null)
                    pointedScript.PointOutEvent();

                // aggiorna l'oggetto puntato
                pointedObject = rayHit.transform.gameObject;

                // trigger del puntamento dentro il nuovo oggetto
                pointedScript = pointedObject.GetComponent<Pointable>();
                if (pointedScript != null)
                    pointedScript.PointInEvent();
            }
        }
        else
        {
            // trigger del puntamento fuori dal vecchio oggetto
            if (pointedScript != null)
                pointedScript.PointOutEvent();

            // aggiorna l'oggetto puntato
            pointedObject = null;
        }
    }
}
