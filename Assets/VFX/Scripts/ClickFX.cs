using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFX : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    GameObject clickRadiusMeshObj;
    GameObject intersectionObj;

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
    Material intersectionMaterial;
    float intersectionAlpha;
    float intersectionMaxAlpha;
    Color clickRadiusColor;

    MothGlow mothGlow;


    bool coolingDown = false;
    bool dashFeedbackPlaying = false;
    bool groundUIPlaying = false;


    private void Start()
    {
        //Get references for Click Radius effect
        clickRadiusMeshObj = Instantiate(clickRadiusMeshObj);
        intersectionObj = clickRadiusMeshObj.transform.GetChild(0).gameObject;
        clickRadiusMeshObj.transform.localScale = Vector3.one * 0.1f;
        clickRadiusMaterial = clickRadiusMeshObj.GetComponent<MeshRenderer>().material;
        clickRadiusColor = clickRadiusMaterial.GetColor("_Tint");
        maxAlpha = clickRadiusMaterial.GetFloat("_Alpha");
        alpha = maxAlpha;

        //References for Ground effect
        intersectionMaterial = intersectionObj.GetComponent<MeshRenderer>().material;
        intersectionMaxAlpha = intersectionMaterial.GetFloat("_Alpha");
        intersectionAlpha = intersectionMaxAlpha;

        mothGlow = MothGlow.instance;

        defaultColor = clickRadiusColor;

        
        

        intersectionObj.SetActive(false);
        clickRadiusMeshObj.SetActive(false);
    }


    public void Activate(float _cooldown)
    {
        coolDownDuration = _cooldown - 0.25f;
        dashFeedbackPlaying = false;
        scaleDuration = coolDownDuration * 0.1f;
        fadeOutDelay = coolDownDuration * 0.5f;
        fadeOutDuration = coolDownDuration * 0.4f;

        clickRadiusMeshObj.SetActive(true);
        intersectionObj.SetActive(true);
        intersectionAlpha = 1f;

        ClickGrowLeanTween();
        IntersectionFadeOut();
        mothGlow.ClickDash(_cooldown);
        clickRadiusMeshObj.transform.position = transform.position;
        coolingDown = true;
       
    }

    #region ClickRefractionAnimation
    void ClickGrowLeanTween()
    {
        LeanTween.scale(clickRadiusMeshObj, Vector3.one * clickRadius, scaleDuration).setEase(tweenType).setOnComplete(ClickFadeOutLeanTween);
        
    }

    void ClickFadeOutLeanTween()
    {

            LeanTween.value(clickRadiusMeshObj, maxAlpha, 0f, fadeOutDuration).setOnUpdate((alpha) =>
            {
                clickRadiusMaterial.SetFloat("_Alpha", alpha);
            }).setDelay(fadeOutDelay).setOnComplete(AnimationComplete).setEaseOutQuad();

        
    }

    void AnimationComplete()
    {
        
        clickRadiusMeshObj.transform.localScale = Vector3.one * 0.1f;
        clickRadiusMaterial.SetFloat("_Alpha", maxAlpha);

        coolingDown = false;
        clickRadiusMeshObj.SetActive(false);
        
    }
    #endregion


    void IntersectionFadeOut()
    {
        LeanTween.value(intersectionObj, intersectionMaxAlpha, 0f, fadeOutDuration).setOnUpdate((groundUIAlpha) =>
        {
            intersectionMaterial.SetFloat("_Alpha", groundUIAlpha);
        }).setDelay(scaleDuration + fadeOutDelay).setEaseOutQuad().setOnComplete(GroundUIAnimationComplete);
    }

    void GroundUIAnimationComplete()
    {
        intersectionMaterial.SetFloat("_Alpha", intersectionMaxAlpha);
        intersectionObj.SetActive(false);
        groundUIPlaying = false;
    }

    void DashFeedbackAnimationComplete()
    {
        intersectionMaterial.SetFloat("_Alpha", intersectionMaxAlpha);
        intersectionObj.SetActive(false);
        dashFeedbackPlaying = false;
    }



}
