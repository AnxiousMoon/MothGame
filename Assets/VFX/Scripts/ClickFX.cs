using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFX : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    GameObject clickRadiusMeshObj, groundUIObj;

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
    Material groundUIMaterial;
    float groundUIAlpha;
    float groundUIMaxAlpha;
    Color clickRadiusColor;

    enum ClickState
    {
        inactive,
        growing,
        fadeingOut
    }

    ClickState clickState = ClickState.inactive;

    bool isActive = false;


    private void Start()
    {
        clickRadiusMeshObj = Instantiate(clickRadiusMeshObj);
        clickRadiusMeshObj.transform.localScale = Vector3.one * 0.1f;
        clickRadiusMaterial = clickRadiusMeshObj.GetComponent<MeshRenderer>().material;
        clickRadiusColor = clickRadiusMaterial.GetColor("_Tint");
        maxAlpha = clickRadiusMaterial.GetFloat("_Alpha");
        alpha = maxAlpha;

        groundUIMaterial = groundUIObj.GetComponent<MeshRenderer>().material;
        groundUIMaxAlpha = groundUIMaterial.GetFloat("_Alpha");
        groundUIAlpha = groundUIMaxAlpha;
        

        groundUIObj.SetActive(false);
        clickRadiusMeshObj.SetActive(false);
    }


    public void Activate()
    {
        if (!isActive)
        {
            clickRadiusMeshObj.SetActive(true);
            groundUIObj.SetActive(true);

            ClickGrowLeanTween();
            GroundUIFadeOut();
            clickRadiusMeshObj.transform.position = transform.position;
            isActive = true;
        }
    }

    #region ClickRefractionAnimation
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
            LeanTween.value(clickRadiusMeshObj, maxAlpha, 0f, fadeOutDuration).setOnUpdate((alpha) =>
            {
                clickRadiusMaterial.SetFloat("_Alpha", alpha);
            }).setDelay(fadeOutDelay).setOnComplete(AnimationComplete).setEaseOutQuad(); ;
            clickState = ClickState.fadeingOut;
        }
    }

    void AnimationComplete()
    {
        
        clickState = ClickState.inactive;
        clickRadiusMeshObj.transform.localScale = Vector3.one * 0.1f;
        clickRadiusMaterial.SetFloat("_Alpha", maxAlpha);

        isActive = false;
        clickRadiusMeshObj.SetActive(false);
        
    }
    #endregion

    void GroundUIFadeOut()
    {
        LeanTween.value(groundUIObj, groundUIMaxAlpha, 0f, fadeOutDuration * 0.5f).setOnUpdate((groundUIAlpha) =>
        {
            groundUIMaterial.SetFloat("_Alpha", groundUIAlpha);
        }).setDelay(fadeOutDelay).setEaseOutQuad().setOnComplete(GroundUIAnimationComplete);
    }

    void GroundUIAnimationComplete()
    {
        groundUIMaterial.SetFloat("_Alpha", groundUIMaxAlpha);
        groundUIObj.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            Activate();
        }
    }
}
