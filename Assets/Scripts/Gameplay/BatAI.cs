using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAI : MonoBehaviour
{

    public Transform[] waypoints;
    public int speed;
    public int maxSpeed = 10;
    public int dashSpeed = 50;
    public GameObject web;
    public GameObject ghost;

    private int waypointIndex;
    private float dist;
    private float distRock;

    Rigidbody rb;

    BatAnimation batAnimation;

    // Start is called before the first frame update
    void Start()
    {
        batAnimation = gameObject.GetComponent<BatAnimation>();

        rb = this.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.LookAt(col.transform.position);
            maxSpeed = 100;
            speed = 100;

            batAnimation.StartDashAnimation();
        }
        if (col.tag == "Web")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.freezeRotation = true;
            speed = 0;
        }
        if (col.tag == "Clicking")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.LookAt(ghost.transform.position);
            maxSpeed = 50;
            speed = 50;
        }
        if (col.tag == "Sound")
        {
            transform.LookAt(web.transform.position);
            {
                maxSpeed = 100;
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            transform.LookAt(col.transform.position);
            speed = 100;
            maxSpeed = 100;
        }
        if (col.tag == "Sound")
        {
            //rb.velocity = Vector3.zero;
            //rb.angularVelocity = Vector3.zero;
            //rb.freezeRotation = true;
            //transform.LookAt(transform.forward);
            //distRock = Vector3.Distance(transform.position, col.transform.position);
            //if (distRock < 1f)
            //{
            transform.LookAt(web.transform.position);
            speed = 100;
            //}
            //speed = 100;
        }
        if (col.tag == "Clicking")
        {
            transform.LookAt(ghost.transform.position);
            maxSpeed = 50;
            speed = 50;
        }
        //if (col.tag == "Stand-in")
        //{
        //    transform.LookAt(waypoints[waypointIndex].position);
        //    Patrol();
        //}
    }

    void OnTriggerExit(Collider col)
    {
        transform.LookAt(waypoints[waypointIndex].position);
        maxSpeed = 10;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Player")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            speed = 100;
            maxSpeed = 100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if (dist < 3f)
        {
            IncreaseIndex();
        }
        Patrol();
    }

    void Patrol()
    {
        rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    void IncreaseIndex()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
        transform.LookAt(waypoints[waypointIndex].position);
        speed = 100;
    }
}
