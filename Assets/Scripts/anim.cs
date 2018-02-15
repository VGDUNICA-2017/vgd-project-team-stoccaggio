using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<Animator>().SetBool("isWalking", true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}