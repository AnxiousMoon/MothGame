using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAI : MonoBehaviour
{

    public Transform[] waypoints;
    public int speed;

    private int waypointIndex;
    private float dist;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        speed = 100;
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
            speed = 100;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            transform.LookAt(col.transform.position);
            speed = 100;
        }
    }

    void OnTriggerExit(Collider col)
    {
        transform.LookAt(waypoints[waypointIndex].position);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Player")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.freezeRotation = true;
            speed = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if (dist < 1f)
        {
            IncreaseIndex();
        }
	Patrol();      
    }

    void Patrol()
    {
        rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, 10f);
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
