using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown {

    // timer
    private float timer;

    // eventi
    public event ExpiretionEventHandler ExpirationHandler;
    public delegate void ExpiretionEventHandler();

    public void Set(int seconds)
    {
        timer = seconds;
    }
    public void Set(int minutes, int seconds)
    {
        timer = minutes * 60 + seconds;
    }

    public int Minutes()
    {
        return Mathf.FloorToInt((float)timer / 60F);
    }

    public int Seconds()
    {
        return Mathf.FloorToInt((float)timer - Minutes() * 60);
    }

    public bool isExpired()
    {
        return timer <= 0;
    }

    public string NiceString()
    {
        return string.Format("{0:0}:{1:00}", Minutes(), Seconds());
    }

    public void Update(float elapsedTime)
    {
        if(timer > 0)
        {
            timer -= elapsedTime;

            // check per vedere se il countdown è scaduto
            if (timer <= 0 && ExpirationHandler != null)
                ExpirationHandler();

            if (timer <= 0)
                timer = 0;
        }
    }
}
