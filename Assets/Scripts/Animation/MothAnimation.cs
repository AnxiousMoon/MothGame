using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothAnimation : MonoBehaviour
{
    private static MothAnimation _instance;
    public static MothAnimation instance { get { return _instance; } }

    [SerializeField] GameObject player;
    Rigidbody playerRB;
    Animator animator;
    [SerializeField] [Range(0f, 1f)] float speedMultiplier = 0.275f;
    [SerializeField] PlaterMoveFXController footPebbleController;
    Vector3 targetMoveDirection;
    private void Awake()
    {
        SingletonCheck();
        animator = gameObject.GetComponent<Animator>();
    }
    private void Start()
    {
        playerRB = player.GetComponent<Rigidbody>();
    }
    private void Update()
    {

        animator.SetFloat("Speed", playerRB.velocity.magnitude * speedMultiplier);
        if (playerRB.velocity.magnitude > 0.1f)
        {
            targetMoveDirection = playerRB.velocity.normalized;
            targetMoveDirection = Vector3.Scale(targetMoveDirection, new Vector3(1f, 0f, 1f));
            
        }

        transform.forward = Vector3.RotateTowards(transform.forward, targetMoveDirection, 0.1f, 0.1f) ;
    }

    public void RightFootImpact()
    {
        footPebbleController.RightFoot();
    }
    public void LeftFootImpact()
    {
        footPebbleController.LeftFoot();
    }

    public void Dash()
    {
        animator.SetBool("isDashing", true);
    }

    public void DashComplete()
    {
        animator.SetBool("isDashing", false);
        Debug.Log("Dsh Complete");
    }

    private void SingletonCheck()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning("Another instance of" + this + " exists, destroying this");
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

}
