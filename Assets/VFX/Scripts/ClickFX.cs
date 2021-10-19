using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFX : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    GameObject clickRadiusMeshObj;

    [SerializeField]
    float clickRadius = 3f;
        
    [SerializeField] [Range (0f,1f)]
    float maxAlpha = 0.5f;

    [Header("Animation")]

    [SerializeField]
    float scaleDuration = 0.2f;
    [SerializeField]
    float fadeOutDelay = 0.5f,
        fadeOutDuration = 0.5f;
    [SerializeField]
    LeanTweenType tweenType;

    float alpha;


    Material clickRadiusMaterial;
    Color clickRadiusColor;

    enum ClickState
    {
        inactive,
        growing,
        fadeingOut
    }

    ClickState clickState = ClickState.inactive;


    private void Start()
    {
        clickRadiusMeshObj = Instantiate(clickRadiusMeshObj);
        clickRadiusMeshObj.transform.localScale = Vector3.one * 0.1f;
        clickRadiusMaterial = clickRadiusMeshObj.GetComponent<MeshRenderer>().material;
        clickRadiusColor = clickRadiusMaterial.color;
        alpha = maxAlpha;

        clickRadiusMeshObj.SetActive(false);
    }


    public void Activate()
    {
        clickRadiusMeshObj.SetActive(true);

        ClickGrowLeanTween();
        clickRadiusMeshObj.transform.position = transform.position;
    }

    void ClickGrowLeanTween()
    {
        if (clickState == ClickState.inactive)
        {
            LeanTween.scale(clickRadiusMeshObj, Vector3.one * clickRadius, scaleDuration).setEase(tweenType).setOnComplete(ClickFadeOutLeanTween);
            clickState = ClickState.growing;
        }
    }

    void ClickFadeOutLeanTween()
    {
        if (clickState == ClickState.growing)
        {
            LeanTween.value(clickRadiusMeshObj, maxAlpha, 0f, 1f).setOnUpdate((alpha) => {
                clickRadiusMaterial.color = new Color(clickRadiusColor.r, clickRadiusColor.g, clickRadiusColor.b, alpha);
            }).setDelay(fadeOutDelay).setOnComplete(AnimationComplete);
            clickState = ClickState.fadeingOut;
        }
    }

    void AnimationComplete()
    {
        
        clickState = ClickState.inactive;
        clickRadiusMeshObj.transform.localScale = Vector3.one * 0.1f;
        clickRadiusMaterial.color = new Color(clickRadiusColor.r, clickRadiusColor.g, clickRadiusColor.b, maxAlpha);

        clickRadiusMeshObj.SetActive(false);
        
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            Activate();
        }
    }
}
