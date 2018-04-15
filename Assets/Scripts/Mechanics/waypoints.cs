using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Transform[] wayPoints;
    public float speed = 4.0f;
    public float minDist = 0.4f;
    public float rotationSpeed = 10.0f;

    private int current = 0;

    private Animator anim;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        anim.SetBool("isWalking", true);
    }

    private void Update()
    {
        wayPoints[current].position = new Vector3(wayPoints[current].position.x, transform.position.y, wayPoints[current].position.z);
        
        if (Vector3.Distance(transform.position, wayPoints[current].position) > minDist)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, wayPoints[current].position, speed * Time.deltaTime);
            rb.MovePosition(pos);
        }
        else
        {
            current = (current + 1) % wayPoints.Length;
            Vector3 dir = (wayPoints[current].position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
