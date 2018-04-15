using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public StoryController StoryController;

    private void Start()
    {
        StoryController = new StoryController();
    }

    // rende il gameObject permanente
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
