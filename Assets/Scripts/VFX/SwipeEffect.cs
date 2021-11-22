using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeEffect : MonoBehaviour
{
    Material material;
    [SerializeField] float fadeDuration = 1f;
    Color startColor;
    private void Awake()
    {
        material = gameObject.GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        startColor = material.GetColor("_BaseColor");
        LeanTween.value(gameObject, 1f, 0f, fadeDuration).setOnUpdate((float _alpha) =>
        {
            material.SetColor("_BaseColor", new Color(startColor.r, startColor.g, startColor.b, _alpha));
        }).setEaseOutQuad().setOnComplete(DestroyMe);
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
