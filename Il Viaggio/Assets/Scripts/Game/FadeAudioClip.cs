using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAudioClip : MonoBehaviour {

    public float fadeSpeed = 0.005f;

    public float volumeLimit = 1.0f;

    public FadeDirection fadeDirection = FadeDirection.None;

    public enum FadeDirection
    {
        None,
        Louder,
        Quieter
    }

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate () {
		
        if(fadeDirection != FadeDirection.None)
        {
            if(fadeDirection == FadeDirection.Louder && audioSource.volume != volumeLimit)
                audioSource.volume += fadeSpeed;
            else if(fadeDirection == FadeDirection.Quieter && audioSource.volume != volumeLimit)
                audioSource.volume -= fadeSpeed;
        }
    }
}
