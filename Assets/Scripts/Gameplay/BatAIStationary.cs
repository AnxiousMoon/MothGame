using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAIStationary : MonoBehaviour
{
    public int speed = 0;
    public int maxSpeed = 0;
    public int dashSpeed = 50;
    public bool caught = false;

    private float dist;

    public GameObject ghost;
    public GameObject returnPoint;
    public GameObject webStandIn;

    public AK.Wwise.Event Web;

    public AkAmbient Sound;

    private Collider b_collider;

    Rigidbody rb;
    BatAnimation batAnimation;

    // Start is called before the first frame update
    void Start()
    {
        batAnimation = gameObject.GetComponent<BatAnimation>();
        b_collider = gameObject.GetComponent<Collider>();

        rb = gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Sound = this.GetComponent<AkAmbient>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Clicking")
        {
            speed = 100;
            maxSpeed = 1;
            transform.LookAt(ghost.transform.position);
            batAnimation.StartDashAnimation();
        }
        if (col.tag == "Sound")
        {
            speed = 100;
            maxSpeed = 1;
            transform.LookAt(webStandIn.transform.position);
        }
        if (col.tag == "Ghost")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.LookAt(returnPoint.transform.position);
            speed = 100;
            maxSpeed = 1;
        }
        if (col.tag == "Player")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.LookAt(col.transform.position);
            speed = 100;
            maxSpeed = 1;
            batAnimation.StartDashAnimation();
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Sound")
        {
            speed = 100;
            maxSpeed = 1;
            transform.LookAt(webStandIn.transform.position);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Wall" || col.collider.tag == "Obstacle")
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
            Destroy(Sound);
            Web.Post(gameObject);
            batAnimation.StartWebAnimation();
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (col.collider.tag == "Full")
        {
            b_collider.enabled = false;
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
