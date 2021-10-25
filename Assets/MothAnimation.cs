using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothAnimation : MonoBehaviour
{
    [SerializeField] GameObject player;
    Rigidbody playerRB;
    Animator animator;
    [SerializeField] [Range(0f, 1f)] float speedMultiplier = 0.275f;
    Vector3 playerMoveDirection;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    private void Start()
    {
        playerRB = player.GetComponent<Rigidbody>();
    }
    private void Update()
    {

        animator.SetFloat("Speed", playerRB.velocity.magnitude * speedMultiplier);
        playerMoveDirection = playerRB.velocity.normalized;
        transform.forward = playerMoveDirection;

    }

}
