using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITrigger : MonoBehaviour
{
    UIFrameAnimation uiFrameAnimation;

    private void Awake()
    {
        uiFrameAnimation = transform.parent.gameObject.GetComponent<UIFrameAnimation>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Dashing")
        {
            uiFrameAnimation.TriggerEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Dashing")
        {
            uiFrameAnimation.TriggerExit();
        }
    }
}
