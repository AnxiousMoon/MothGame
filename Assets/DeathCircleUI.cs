using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCircleUI : MonoBehaviour
{
    [SerializeField] Vector3 targetScale = Vector3.zero;
    [SerializeField] float scaleDuration = 1f;
    public void Activate()
    {
        LeanTween.scale(gameObject,targetScale,scaleDuration);
    }
}
