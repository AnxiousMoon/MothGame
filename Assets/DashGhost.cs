using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashGhost : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void FadeIn()
    {
        animator.SetBool("IsActive", true);
    }
}
