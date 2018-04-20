using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxMover : MonoBehaviour {

    public float rotationSpeed = 0.2f;

	void Update () {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
	}
}
