using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float dashSpeed = 100f;
    public float cooldown = 2f;
    public Collider collider;

    float horiziontalMovement;
    float verticalMovement;
    float rbDrag = 6f;
    float timePassed;
    float dashCooldown;

    Vector3 moveDirection;

    Rigidbody rb;

    MothAnimation mothAnimation;

    //bool to control whether player can move during death animation and initial camera pan.
    bool canMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Physics.gravity = new Vector3(0, -50, 0);
        mothAnimation = MothAnimation.instance;
    }

    private void Update()
    {
        myInput();
        ControlDrag();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCooldown <= 0)
            {
                rb.AddForce(moveDirection * dashSpeed, ForceMode.VelocityChange);
                gameObject.tag = "Dashing";
                collider.enabled = true;
                timePassed = 0;
                dashCooldown = cooldown;

                mothAnimation.Dash(cooldown);
            }
        }
        timePassed += Time.deltaTime;
        dashCooldown -= Time.deltaTime;
        if (timePassed >= 1)
        {
            gameObject.tag = "Player";
        }
        if (timePassed >= 2)
        {
            collider.enabled = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Enemy")
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        if (col.collider.tag == "Web")
        {
            if(gameObject.tag == "Dashing")
            {
                rb.AddForce(moveDirection * dashSpeed, ForceMode.VelocityChange);
            }
        }
    }

    void myInput()
    {
        horiziontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horiziontalMovement;

    }



    void ControlDrag()
    {
        rb.drag = rbDrag;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Acceleration);
    }

    public void AllowPlayerMovement(bool _bool)
    {
        canMove = _bool;
    }
    //public float zAcceleration; // Acceleration on the Z-Axis
    //public float zVelocity; // Velocity on the Z-Axis
    //public float xAcceleration; // Acceleration on the X-Axis
    //public float xVelocity; // Velocity on the X-Axis
    //public float yAcceleration;
    //public float xVelMax = 0.05f;
    //public float zVelMax = 0.05f;
    //public bool stealth = false;
    //public bool canJump = true;
    //public float m; // Mass
    //public float f; // Force
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //    if (Input.GetKey("d"))
    //    {
    //        zAcceleration += 0.01f;
    //        zVelocity += zAcceleration;
    //        if (zVelocity > zVelMax)
    //        {
    //            zVelocity = zVelMax;
    //        }
    //        if (Input.GetKeyDown(KeyCode.LeftShift))
    //        {
    //            if (xVelocity > 0)
    //            {
    //                xVelocity += 50.0f;
    //            }
    //            if (zVelocity > 0)
    //            {
    //                zVelocity += 50.0f;
    //            }
    //        }

    //        transform.Translate(Vector3.back * zVelocity);
    //    }

    //    if (Input.GetKey("a"))
    //    {
    //        zAcceleration += 0.01f;
    //        zVelocity += zAcceleration;
    //        if (zVelocity > zVelMax)
    //        {
    //            zVelocity = zVelMax;
    //        }
    //        if (Input.GetKeyDown(KeyCode.LeftShift))
    //        {
    //            if (xVelocity > 0)
    //            {
    //                xVelocity += 50.0f;
    //            }
    //            if (zVelocity > 0)
    //            {
    //                zVelocity += 50.0f;
    //            }
    //        }
    //        transform.Translate(Vector3.forward * zVelocity);
    //    }

    //    if (Input.GetKey("s"))
    //    {
    //        xAcceleration += 0.01f;
    //        xVelocity += xAcceleration;
    //        if (xVelocity > xVelMax)
    //        {
    //            xVelocity = xVelMax;
    //        }
    //        if (Input.GetKeyDown(KeyCode.LeftShift))
    //        {
    //            if (xVelocity > 0)
    //            {
    //                xVelocity += 50.0f;
    //            }
    //            if (zVelocity > 0)
    //            {
    //                zVelocity += 50.0f;
    //            }
    //        }
    //        transform.Translate(Vector3.left * xVelocity);
    //    }

    //    if (Input.GetKey("w"))
    //    {
    //        xAcceleration += 0.01f;
    //        xVelocity += xAcceleration;
    //        if (xVelocity > xVelMax)
    //        {
    //            xVelocity = xVelMax;
    //        }
    //        if (Input.GetKeyDown(KeyCode.LeftShift))
    //        {
    //            if (xVelocity > 0)
    //            {
    //                xVelocity += 50.0f;
    //            }
    //            if (zVelocity > 0)
    //            {
    //                zVelocity += 50.0f;
    //            }
    //        }
    //        transform.Translate(Vector3.right * xVelocity);
    //    }

    //    //if (Input.GetKeyDown("c") && !stealth)
    //    //{
    //    //    stealth = true;
    //    //}
    //    //else if (Input.GetKeyDown("c") && stealth)
    //    //{
    //    //    stealth = false;
    //    //}

    //    //if (stealth)
    //    //{
    //    //    xVelMax = 0.025f;
    //    //    zVelMax = 0.025f;
    //    //}
    //    //else if (!stealth)
    //    //{
    //    //    xVelMax = 0.1f;
    //    //    zVelMax = 0.1f;
    //    //}

    //    //if (Input.GetKey("space"))
    //    //{
    //    //    yAcceleration = 5.0f;
    //    //    transform.Translate(Vector3.up * yAcceleration);
    //    //    if ()
    //    //    {

    //    //    }
    //    //}

    //    //if (Input.GetKey(KeyCode.LeftShift) && !stealth)
    //    //{
    //    //    xVelMax = 0.2f;
    //    //    zVelocity = 0.2f;
    //    //}
    //    //else if (Input.GetKey(KeyCode.LeftShift) && stealth)
    //    //{
    //    //    xVelMax = 0.05f;
    //    //    zVelocity = 0.05f;
    //    //}

    //    zAcceleration = 0;
    //    zVelocity -= 0.0005f;
    //    xAcceleration = 0;
    //    xVelocity -= 0.0005f;
    //    if (zVelocity < 0)
    //        zVelocity = 0;
    //    if (xVelocity < 0)
    //        xVelocity = 0;
    //}
}
