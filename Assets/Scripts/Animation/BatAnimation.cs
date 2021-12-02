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

    [Header("Swipe Effect")]
    [SerializeField] [Tooltip("Reference GameObject with swipe effect attached")] GameObject swipeVFXObj;
    [SerializeField] [Tooltip("Duration swipe effect should appear for.")] float swipeFXDuration = 0.2f;

    [Header("Death Effect")]
    [SerializeField] [Tooltip("Reference to death particle effect on bat gameobject")] ParticleSystem deathFX;

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

    public void StopDashAnimation()
    {
        animator.SetBool("isDashing", false);
    }
    public void StartWebAnimation()
    {
        animator.SetBool("isWebbed", true);
    }

    public void ShowSwipeFX()
    {
        GameObject SwipeVFX = Instantiate(swipeVFXObj,transform);
        SwipeVFX.transform.parent = null;
    }

    public void Death()
    {
        deathFX.Play();
    }
}
