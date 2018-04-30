using UnityEngine;
public class FlashingLight : MonoBehaviour
{
    private Light myLight;
    public float pulseSpeed = 1f; //this is in seconds
    private float timer;

    void Start()
    {
        myLight = GetComponent<Light>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > pulseSpeed)
        {
            timer = 0;
            myLight.enabled = !myLight.enabled;
        }
    }
}
 



