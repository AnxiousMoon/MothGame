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

    float coolDownDuration = 10f;

    [SerializeField]
    [ColorUsage(true, true)] Color negativeDashColor;
    Color defaultColor;

    [Header("Animation")]

    [SerializeField]
    float scaleDuration = 0.2f;
    [SerializeField]
    float fadeOutDuration = 3f;
    float fadeOutDelay;
    [SerializeField]
    LeanTweenType tweenType;

    

    float alpha;


    Material clickRadiusMaterial;
    Material groundUIMaterial;
    float groundUIAlpha;
    float groundUIMaxAlpha;
    Color clickRadiusColor;

    MothGlow mothGlow;

    enum ClickState
    {
        inactive,
        growing,
        fadingOut
    }

    ClickState clickState = ClickState.inactive;

    bool coolingDown = false;
    bool dashFeedbackPlaying = false;
    bool groundUIPlaying = false;


    private void Start()
    {
        //Get references for Click Radius effect
        clickRadiusMeshObj = Instantiate(clickRadiusMeshObj);
        clickRadiusMeshObj.transform.localScale = Vector3.one * 0.1f;
        clickRadiusMaterial = clickRadiusMeshObj.GetComponent<MeshRenderer>().material;
        clickRadiusColor = clickRadiusMaterial.GetColor("_Tint");
        maxAlpha = clickRadiusMaterial.GetFloat("_Alpha");
        alpha = maxAlpha;

        //References for Ground effect
        groundUIMaterial = groundUIObj.GetComponent<MeshRenderer>().material;
        groundUIMaxAlpha = groundUIMaterial.GetFloat("_Alpha");
        groundUIAlpha = groundUIMaxAlpha;

        mothGlow = MothGlow.instance;

        defaultColor = clickRadiusColor;

        
        

        groundUIObj.SetActive(false);
        clickRadiusMeshObj.SetActive(false);
    }


    public void Activate(float _cooldown)
    {
        coolDownDuration = _cooldown;
        fadeOutDelay = coolDownDuration - fadeOutDuration;
        if (!coolingDown)
        {
            clickRadiusMeshObj.SetActive(true);
            groundUIObj.SetActive(true);
            groundUIMaterial.SetColor("_Albedo", defaultColor);

            ClickGrowLeanTween();
            GroundUIFadeOut(false);
            mothGlow.ClickDash(_cooldown);
            clickRadiusMeshObj.transform.position = transform.position;
            coolingDown = true;
        }
        else if(!dashFeedbackPlaying && !groundUIPlaying)
        {
            NoDashFeedback();
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
            }).setDelay(fadeOutDelay).setOnComplete(AnimationComplete).setEaseOutQuad();
            clickState = ClickState.fadingOut;
        }
    }

    void AnimationComplete()
    {
        
        clickState = ClickState.inactive;
        clickRadiusMeshObj.transform.localScale = Vector3.one * 0.1f;
        clickRadiusMaterial.SetFloat("_Alpha", maxAlpha);

        coolingDown = false;
        mothGlow.EndCooldown();
        clickRadiusMeshObj.SetActive(false);
        
    }
    #endregion

    void GroundUIFadeOut(bool dashFeedback)
    {
        if (!dashFeedback)
        {
            groundUIPlaying = true;
            LeanTween.value(groundUIObj, groundUIMaxAlpha, 0f, fadeOutDuration * 0.1f).setOnUpdate((groundUIAlpha) =>
            {
                groundUIMaterial.SetFloat("_Alpha", groundUIAlpha);
            }).setDelay(0.5f).setEaseOutQuad().setOnComplete(GroundUIAnimationComplete);
        }
        else
        {
            LeanTween.value(groundUIObj, 0.6f, 0f,  0.3f).setOnUpdate((groundUIAlpha) =>
            {
                groundUIMaterial.SetFloat("_Alpha", groundUIAlpha);
            }).setEaseOutQuad().setOnComplete(DashFeedbackAnimationComplete);
        }
    }

    void GroundUIAnimationComplete()
    {
        groundUIMaterial.SetFloat("_Alpha", groundUIMaxAlpha);
        groundUIObj.SetActive(false);
        groundUIPlaying = false;
    }

    void DashFeedbackAnimationComplete()
    {
        groundUIMaterial.SetFloat("_Alpha", groundUIMaxAlpha);
        groundUIObj.SetActive(false);
        dashFeedbackPlaying = false;
    }

    void NoDashFeedback()
    {
        dashFeedbackPlaying = true;
        Debug.Log("Dash is cooling down");

        groundUIObj.SetActive(true);
        groundUIMaterial.SetColor("_Albedo", negativeDashColor);
        
        LeanTween.cancel(groundUIObj);


        mothGlow.NoDashFeedback(negativeDashColor);
        GroundUIFadeOut(true);
    }

}
