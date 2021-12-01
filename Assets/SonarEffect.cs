using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarEffect : MonoBehaviour
{
    Vector3 targetScale = new Vector3(1, 1, 1);
    [SerializeField] float scaleDuration = 1f;
    [SerializeField] float fadeOutDuration = 0.5f;
    Material material;

    private void Awake()
    {
        material = gameObject.GetComponent<MeshRenderer>().material;
        
    }

    private void Start()
    {
        transform.localScale = Vector3.zero + Vector3.one * 0.01f;;
    }
    public void Activate()
    {
        material.SetFloat("_Alpha", 1f);
        LeanTween.scale(gameObject, targetScale, scaleDuration);
    }
    public void Deactivate()
    {
        LeanTween.value(gameObject, 1f, 0f, fadeOutDuration).setOnUpdate((float _alpha) =>
        {
            material.SetFloat("_Alpha", _alpha);
        }).setEaseOutQuad().setOnComplete(ScaleAway);
    }
    void ScaleAway()
    {
        transform.localScale = Vector3.zero + Vector3.one * 0.01f;
    }
}
