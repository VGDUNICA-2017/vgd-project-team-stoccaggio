using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public float amplitude = 0.1f;
    public bool shakeOnStart = false;

    private bool isActive = false;

    void Start()
    {
        if (shakeOnStart) StartShake();        
    }

    public void StartShake()
    {
        if(!isActive)
        {
            isActive = true;
            StartCoroutine(shakeCamera());
        }
    }

    public void StopShake()
    {
        isActive = false;
    }

    private IEnumerator shakeCamera()
    {
        Vector3 cameraOrigin = transform.localPosition;
        Vector2 offsetValues = new Vector2();

        while (isActive)
        {
            offsetValues = Random.insideUnitCircle * amplitude;

            transform.localPosition = new Vector3(offsetValues.x, cameraOrigin.y + offsetValues.y - 0.5f, cameraOrigin.z);

            yield return null;
        }

        transform.localPosition = cameraOrigin;
    }
}
