using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAnimation : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isDashing", false);
    }

    public void StartDashAnimation()
    {
        Debug.Log("Start Dash Animation");
        animator.SetBool("isDashing", true);
    }
}
