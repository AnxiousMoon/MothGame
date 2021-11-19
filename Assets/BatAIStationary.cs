using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAIStationary : MonoBehaviour
{
    public int speed = 0;
    public int maxSpeed = 0;
    public int dashSpeed = 50;
    public GameObject web;
    public GameObject ghost;
    public bool caught = false;

    private float dist;

    public GameObject returnPoint;

    Rigidbody rb;
    BatAnimation batAnimation;

    // Start is called before the first frame update
    void Start()
    {
        batAnimation = gameObject.GetComponent<BatAnimation>();

        rb = gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Clicking")
        {
            speed = 100;
            maxSpeed = 10;
            transform.LookAt(ghost.transform.position);
        }
        if (col.tag == "Ghost")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.LookAt(returnPoint.transform.position);
            speed = 100;
            maxSpeed = 10;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Wall")
        {
            caught = true;
            transform.LookAt(returnPoint.transform.position);
        }
        if (col.collider.tag == "Web")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.freezeRotation = true;
            speed = 0;
        }

    }

    void OnTriggerExit(Collider col)
    {
        caught = true;
        transform.LookAt(returnPoint.transform.position);
    }

    void Update()
    {
        rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        if (caught)
        {
            dist = Vector3.Distance(transform.position, returnPoint.transform.position);
            if (dist < 3f)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                maxSpeed = 0;
                speed = 0;
                transform.position = returnPoint.transform.position;
                transform.rotation = returnPoint.transform.rotation;
                caught = false;
            }
        }
    }
}
