using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private StoryController storyController;

    private void Start()
    {
        storyController = new StoryController();
    }
    // rende il gameObject permanente
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
