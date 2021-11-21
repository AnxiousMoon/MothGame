using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPadRadius : MonoBehaviour
{
    Vector3 targetScale = new Vector3(20,8,20);
    [SerializeField] float scaleDuration = 1f;

    private void Start()
    {
        transform.localScale = Vector3.zero + Vector3.one * 0.01f;
    }
    public void Activate()
    {
        LeanTween.scale(gameObject, targetScale, scaleDuration);
    }
    public void Deactivate()
    {
        transform.localScale = Vector3.zero + Vector3.one * 0.01f;
    }
}
