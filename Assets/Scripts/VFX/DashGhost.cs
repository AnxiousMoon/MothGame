using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashGhost : MonoBehaviour
{
    Animator animator;
    DashFX parentDash;
    float defaultAlpha = 0.5f;
    Color defaultColor = Color.magenta;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        parentDash = transform.parent.gameObject.GetComponent<DashFX>();
        defaultColor = gameObject.GetComponent<MeshRenderer>().material.color;
        defaultColor = new Color(defaultColor.r, defaultColor.g, defaultColor.b, defaultAlpha);
    }

    public void FadeIn()
    {

            Debug.Log(gameObject.name);
    
        animator.SetBool("IsActive", true);
    }

    public void AnimationComplete()
    {
        animator.SetBool("IsActive", false);
        gameObject.SetActive(false);
    }

}
