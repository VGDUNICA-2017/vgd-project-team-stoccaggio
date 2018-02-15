using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointable : MonoBehaviour {

    // variabili
    public Shader pointedShader;
    public UnityEngine.UI.Text actionTextbox;
    public UnityEngine.UI.Text actionSubTextbox;
    public string pointedText;
    public string pointedSubText;

    // eventi
    public event ActionEventHandler ActionHandler;
    public delegate void ActionEventHandler();

    // componenti
    private Material mat;
    private Shader originalShader;

    // supporto
    private bool isPointed = false;

    private void Start()
    {
        // inizializzazione shader
        mat = GetComponent<Renderer>().material;
        originalShader = mat.shader;
    }

    private void Update()
    {
        if (isPointed && Input.GetKeyDown(KeyCode.F))
            if(ActionHandler != null) ActionHandler();
    }

    public void PointInEvent()
    {
        isPointed = true;

        // aggiorna lo shader
        if (pointedShader != null)
            mat.shader = pointedShader;

        // aggiorna il testo
        if (actionTextbox != null)
            actionTextbox.text = pointedText;
        if (actionSubTextbox != null)
            actionSubTextbox.text = pointedSubText;
    }

    public void PointOutEvent()
    {
        isPointed = false;

        // aggiorna lo shader
        if (pointedShader != null)
            mat.shader = originalShader;

        // aggiorna il testo
        if (actionTextbox != null)
            actionTextbox.text = "";
        if (actionSubTextbox != null)
            actionSubTextbox.text = "";
    }
}
