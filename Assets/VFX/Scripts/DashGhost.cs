using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashGhost : MonoBehaviour
{

    [SerializeField] float fadeDuration = 0.5f;
    [SerializeField] float fadeDelay = 0.5f;
    float defaultAlpha = 0.5f;
    Color defaultColor = Color.magenta;

    MeshRenderer meshRenderer;
    Material[] materials;

    private void Awake()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        materials = meshRenderer.materials;
    }

    private void Start()
    {
        defaultAlpha = materials[0].GetFloat("_Alpha");
        FadeOut();
    }

    public void FadeOut()
    {
        LeanTween.value(gameObject, defaultAlpha, 0f, fadeDuration).setOnUpdate((float _alpha) =>
        {
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].SetFloat("_Alpha", _alpha);
            }
        }).setEaseOutQuad().setOnComplete(AnimationComplete).setDelay(fadeDelay);

    }

    public void AnimationComplete()
    {
        Destroy(gameObject);
    }

}
