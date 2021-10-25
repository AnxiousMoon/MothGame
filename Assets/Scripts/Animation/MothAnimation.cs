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
    [SerializeField] PlayerMoveFXController playerMoveFXController;
    [SerializeField] ClickFX clickDashScript;
    Vector3 targetMoveDirection;

    enum PlayerState
    {
        idle,
        walking,
        dashing
    }

    bool walkFXPlaying = false;

    PlayerState playerState = PlayerState.idle;
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
            

            playerState = PlayerState.walking;

        }
        else
        {
            playerState = PlayerState.idle;
            
        }

        transform.forward = Vector3.RotateTowards(transform.forward, targetMoveDirection, 0.1f, 0.1f) ;
    }

    public void RightFootImpact()
    {
        playerMoveFXController.RightFoot();
    }
    public void LeftFootImpact()
    {
        playerMoveFXController.LeftFoot();
    }

    void PlayWalkFX()
    {
        if (!walkFXPlaying)
        {
            playerMoveFXController.PlayDust();
            walkFXPlaying = true;
            Debug.Log("Start FX");
        }
    }

    void StopWalkFX()
    {
        if (walkFXPlaying)
        {
            playerMoveFXController.StopDust();
            walkFXPlaying = false;
            Debug.Log("Stop FX");

        }
    }

    public void Dash(float _cooldown)
    {
        animator.SetBool("isDashing", true);
        playerState = PlayerState.dashing;
        StopWalkFX();
        clickDashScript.Activate(_cooldown);
    }

    public void DashComplete()
    {
        animator.SetBool("isDashing", false);
        playerState = PlayerState.walking;
        PlayWalkFX();
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
