using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAnimation : MonoBehaviour
{
    Animator animator;
    [Header("Animation Speeds")]
    [Tooltip ("Controls the speed at which the idle animation plays. 1 is default.")]
    [SerializeField] float wingFlapSpeed = 1f;
    [Tooltip("The dash animation plays when the bat chases the player.")]
    [SerializeField] float dashSpeed = 5f;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        
    }

    private void Start()
    {
        animator.SetBool("isDashing", false);
        animator.SetFloat("idleSpeed", wingFlapSpeed);
        animator.SetFloat("dashSpeed", dashSpeed);
    }

    public void StartDashAnimation()
    {
        Debug.Log("Start Dash Animation");
        animator.SetBool("isDashing", true);
    }
}
