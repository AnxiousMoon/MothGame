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
    private Collider b_collider;

    public AK.Wwise.Event Web; // A Wise Event That is Triggered When The Bat is Caught in a Web.

    public AK.Wwise.Event Free; // Sounds Used by Wise to Play The Sounds of the Bats Wings Flapping.

    Rigidbody rb;

    BatAnimation batAnimation;

    Dissolve dissolve;


    // Start is called before the first frame update
    void Start()
    {
        batAnimation = gameObject.GetComponent<BatAnimation>();
        b_collider = gameObject.GetComponent<Collider>();
        dissolve = transform.GetChild(0).GetComponent<Dissolve>(); //Gets dissolve component from the first child ie the bat mesh

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
            maxSpeed = 10;
            speed = 10;

            batAnimation.StartDashAnimation();
        }
        if (col.tag == "Clicking")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            maxSpeed = 5;
            speed = 100;
            maxSpeed = 10;
            transform.LookAt(ghost.transform.position);
        }
        if (col.tag == "Ghost")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.LookAt(waypoints[waypointIndex].position);
            speed = 100;
            maxSpeed = 10;
        }
        if (col.tag == "Sound")
        {
            transform.LookAt(web.transform.position);
            maxSpeed = 10;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            transform.LookAt(col.transform.position);
            speed = 100;
            maxSpeed = 5;
            batAnimation.StartDashAnimation();
        }
        //if (col.tag == "Sound")
        //{
        //    //rb.velocity = Vector3.zero;
        //    //rb.angularVelocity = Vector3.zero;
        //    //rb.freezeRotation = true;
        //    //transform.LookAt(transform.forward);
        //    //distRock = Vector3.Distance(transform.position, col.transform.position);
        //    //if (distRock < 1f)
        //    //{
        //    transform.LookAt(web.transform.position);
        //    speed = 100;
        //    //}
        //    //speed = 100;
        //}
        //if (col.tag == "Clicking")
        //{
        //    transform.LookAt(ghost.transform.position);
        //    maxSpeed = 5;
        //    speed = 100;
        //}
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
        }
        if (col.collider.tag == "Wall" || col.collider.tag == "Obstacle")
        {
            maxSpeed = 5;
            speed = 100;
            IncreaseIndex();
        }
        if (col.collider.tag == "Web" || col.collider.tag == "Stuck")
        {
            b_collider.enabled = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.freezeRotation = true;
            speed = 0;
            batAnimation.StartWebAnimation();
            Free.Stop(gameObject); // Stop ambient Bat sounds as the Bat is stuck in Web
            Web.Post(gameObject);
        }
        if (col.collider.tag == "Full")
        {
            IncreaseIndex();
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (col.collider.tag == "Wall" || col.collider.tag == "Obstacle")
        {
            maxSpeed = 5;
            speed = 100;
            IncreaseIndex();
        }
        if (col.collider.tag == "Stuck" || col.collider.tag == "Full")
        {
            b_collider.enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
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
        rb.AddForce(transform.forward * speed  * 2f, ForceMode.Acceleration);
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

    public void Kill()
    {
        if (dissolve)
        {
            dissolve.DissolveMe(1f, true); //dissolve for 1s then deactivate
        }
        else
        {
            Debug.LogError("There is no dissolve script attached to this bat - use stationary bat prefab");
        }
        batAnimation.Death();
        Web.Stop(gameObject);
    }
}
