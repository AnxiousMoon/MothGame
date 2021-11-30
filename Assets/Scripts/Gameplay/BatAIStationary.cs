using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAIStationary : MonoBehaviour
{
    public int speed = 0; // Bats Current Speed.
    public int chaseSpeed = 100; // The Speed of the Bat When Chasing The Player.
    public int stationairySpeed = 0; // Variable Used to Return the Bats Speed to 0.
    public int maxSpeed = 0; // The Current Maximum Allowed Speed of the Bat.
    public int maxChaseSpeed = 1; // The Allowed Maximum Speed of the Bat During a Chase.
    public int dashSpeed = 50; // The Speed of the Bat When Attacking the Player

    public bool caught = false; // A Bool Used to Check if the Bat has Caught the Ghost.

    public GameObject ghost; // A GameObject Used To Access The Positions and Collider of the Ghost.
    public GameObject returnPoint; // A GameObject Set to the Origin Point of the Bat. Used to Return it to its Original Position.
    public GameObject webStandIn; // A GameObject Used to Stand-In for Webs When Soundpads Are Triggered. Using a Stand-In Rather Than the Actual Web Prevents Game Crashes When the Web is Destroyed.

    public AK.Wwise.Event Web; // A Wise Event That is Triggered When The Bat is Caught in a Web.

    public AkAmbient Sound; // Ambient Sounds Used by Wise to Play The Sounds of the Bats Wings Flapping.

    private Collider b_collider; // A Collider Variable Used to Access The Bats Collider.

    private float dist; // A Float to Check the Distance Between the Bat and its Desired Destination.

    Rigidbody rb; // A Rigidbody Variable Used to Access The Rigidbody Component of the Bat.
    BatAnimation batAnimation; // A Variable Used To Assign An Animation to the Bat Model.

    [SerializeField] Dissolve dissolve; // Reference to dissolve script on bat model

    // Start is called before the first frame update
    void Start()
    {
        batAnimation = gameObject.GetComponent<BatAnimation>(); // Sets Bat Animation to the Component Attached to the GameObject.
        b_collider = gameObject.GetComponent<Collider>(); // Sets Bat Collider Variable to the Component Attached to the GameObject.

        rb = gameObject.GetComponent<Rigidbody>(); // Sets Rigidbody to the Component Attached to the GameObject.
        rb.freezeRotation = true; // Freezes the Rotation of the Bats Rigidbody to Prevent Accidental Rotation.

        Sound = this.GetComponent<AkAmbient>(); // Sets the Sound Variable to the Wise Component Attached to the GameObject
    }

    // On Trigger Enter Checks When the GameObject Enters Other Triggers and Assigns Behaviours to the GameObject.
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Clicking") // Check if the Trigger Box is Tagged as "Clicking". This Trigger Box is Activated When the Player Dashes.
        {
            speed = chaseSpeed; // Speed is Set to 100. As the Velocity of the Bat is Calculated With Physics, Setting a High Speed Reduces the Time to Accelerate to Max Speed.
            maxSpeed = maxChaseSpeed; // Max Speed is Set to 1. This Keeps The Bat at the Lowest Possible Speed and Allows the Player To Maneuver Around Them.
            transform.LookAt(ghost.transform.position); // Sets the Bats Movement Direction to the Player Ghosts Position.
            batAnimation.StartDashAnimation(); // Plays the Dash Animation Assigned to the Bat.
        }
        if (col.tag == "Sound") // Check if the Trigger Box is Tagged as "Sound". This Trigger Box is Activated When the Player Steps on a Soundpad.
        {
            speed = chaseSpeed; // Speed is set to 100.
            maxSpeed = maxChaseSpeed; // Max Speed is set to 1.
            transform.LookAt(webStandIn.transform.position); // Bats Movement Direction Set to Web Stand-In Position.
        }
        if (col.tag == "Ghost") // Check if the Trigger Box is Tagged as "Ghost". This Trigger Box is Activated When the Player Dashes.
        {
            rb.velocity = Vector3.zero; // Sets the Velocity of the Bat to 0.
            rb.angularVelocity = Vector3.zero; // Sets the Rotational Velocity of the Bat to 0.
            transform.LookAt(returnPoint.transform.position); // Bats Movement Direction Set to its Original Position.
            speed = chaseSpeed; // Speed is set to 100.
            maxSpeed = maxChaseSpeed; // Max Speed is set to 1.
        }
        if (col.tag == "Player") // Check if the Trigger Box is Tagged as "Player". This Trigger Box is Constantly Active and Attached to the Player in Order to Detect When the Player is in Kill Range.
        {
            rb.velocity = Vector3.zero; // Sets the Velocity of the Bat to 0.
            rb.angularVelocity = Vector3.zero; // Sets the Rotational Velocity of the Bat to 0.
            transform.LookAt(col.transform.position); // Bats Movement Direction Set to the Location of the Collider (the Player).
            speed = chaseSpeed; // Speed is set to 100.
            maxSpeed = maxChaseSpeed; // Max Speed is set to 1.
            batAnimation.StartDashAnimation(); // Plays the Dash Animation Assigned to the Bat.
        }
    }

    // On Trigger Stay Checks if the GameObject has Reamined Within a Trigger Box.
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Sound") // Check if the Trigger Box is Tagged as "Sound". This Trigger Box is Activated When the Player Steps on a Soundpad.
        {
            speed = chaseSpeed; // Speed is set to 100.
            maxSpeed = maxChaseSpeed; // Max Speed is set to 1.
            transform.LookAt(webStandIn.transform.position); // Bats Movement Direction Set to Web Stand-In Position.

            // By utilizing on trigger stay for the sound trigger, we can be sure that the bat will not deviate from its path and will collide with the web.
            // As the stationary bats are essentially tutorials the intention for them is to die in specific ways. This means that the one bat that collides
            // with the web should always die to that web. Regardless of what the player does to prevent it. Using this method allows the bat to ignore the
            // players ghost once the sound pad has been triggered.+
        }
    }

    // On Collision Enter Checks if the GameObjects Collider has Made Contact With Another Collider.
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Wall" || col.collider.tag == "Obstacle") // Check if the Collider is Tagged as Either "Wall" or "Obstacle".
        {
            caught = true; // Set Caught to True in Order To Reset The Bat. This Prevents it From Getting Stuck in a Part of the Map.
            transform.LookAt(returnPoint.transform.position); // Set the Bats Movement Direction to the Bats Initial Position.
        }
        if (col.collider.tag == "Web") // Check if the Collider is Tagged as "Web".
        {
            rb.velocity = Vector3.zero; // Sets the Velocity of the Bat to 0.
            rb.angularVelocity = Vector3.zero; // Sets the Rotational Velocity of the Bat to 0.
            rb.constraints = RigidbodyConstraints.FreezePosition; // Freezes the Position of the Bat.
            rb.freezeRotation = true; // Freezes the Rotation of the Bat.
            speed = stationairySpeed; // Speed is Set to 0.
            Destroy(Sound); // Destroys the Wing Flapping Sound to Prevent it From Playing.
            Web.Post(gameObject); // Posts the Web Stuck Sound as the Bat is now Caught in the Web.
            batAnimation.StartWebAnimation(); // Plays the Animation Assigned to the Bat Being Caught in the Web.
        }
    }

    // On Collision Enter Checks if the GameObjects Collider has Reamained in Contact With Another Collider.
    void OnCollisionStay(Collision col)
    {
        if (col.collider.tag == "Stuck" || col.collider.tag == "Full") // Check if the Collider is Tagged as Either "Stuck" or "Full".
        {
            b_collider.enabled = false; // Disable the Bats Collider.

            // This theoretically should never happen to any of the stationairy bats. As they are in very scripted and intentional placements.
            // However, players have been known to break things and cause problems outside of intended design. As such, this has been included.
            // The intention of this code is to check if a bat is already caught in the web. If there is a single bat then the web will be tagged
            // as stuck. If another bat is caught while the web is tagged as stuck then the web will be full. This prevents the player from stacking
            // every bat they can into a single web.
        }
    }

    // On Trigger Exit Checks if the GameObject Leaves a Trigger Box.
    void OnTriggerExit(Collider col)
    {
        caught = true; // Set Caught to True in Order To Reset The Bat.
        transform.LookAt(returnPoint.transform.position); // Set the Bats Movement Direction to the Bats Initial Position.
    }

    // FixedUpdate Updates the GameObject in-Step With the Physics Engine.
    void FixedUpdate()
    {
        rb.AddForce(transform.forward * speed * 4f, ForceMode.Acceleration); // Calculates and Applies Speed at Which the Bat Will Move.
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed); // Clamps the Maximum Value of the Velocity of the Bat. Preventing it from Exceeding the Declared Maximum.

        if (caught) // Checks if the Bat has Caught the Ghost.
        {
            dist = Vector3.Distance(transform.position, returnPoint.transform.position); // Calculates the Distance Between the Bats Current Position and the Bats Initial Position.

            if (dist < 3f) // Checks if the Distance is Lower Than 3.
            {
                rb.velocity = Vector3.zero; // Sets the Velocity of the Bat to 0.
                rb.angularVelocity = Vector3.zero; // Sets the Rotational Velocity of the Bat to 0.
                maxSpeed = stationairySpeed; // Max Speed is Set to 0.
                speed = stationairySpeed; // Speed is Set to 0.
                transform.position = returnPoint.transform.position; // The Bats Position is Set to its Initial Position.
                transform.rotation = returnPoint.transform.rotation; // The Bats Rotation is Set to its Initial Rotation.
                caught = false; // Reset the Caught Bool.
            }
        }
    }

    //Innes added this function- ask if you need me to clarify any of the code :)
    //Triggered by web.cs, when the player dashes into a web with a bat trapped in it
    public void Kill()
    {
        dissolve.DissolveMe(1f,true); //dissolve for 1s then deactivate
        batAnimation.Death();
    }
}
