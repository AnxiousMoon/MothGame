using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothGlow : MonoBehaviour
{
    //Singleton
    private static MothGlow _instance;
    public static MothGlow instance { get { return _instance; } }


    [SerializeField][ColorUsage(true,true)] Color glowColor = new Color(63f / 255f, 220f / 255f, 203f / 255f, 1f);
    [SerializeField]
    float glowDuration = 2f, glowDelay = 8f;
    [SerializeField]
    DustParticles dustParticles;
    Material wingMaterial;
    Color currentWingEmissionColor = Color.black;
    float colorLerpValue = 0f;

    bool coolDown = false;
    bool NoDashFeedbackPlaying = false;
    int DashRechargeID;

    private void Awake()
    {
        SingletonCheck();
    }
    private void Start()
    {
        wingMaterial = gameObject.GetComponent<SkinnedMeshRenderer>().materials[2];
        wingMaterial.EnableKeyword("_EMISSION");
        wingMaterial.SetColor("_EmissionColor", glowColor);
    }


    public void ClickDash()
    {
        if (!coolDown)
        {
            wingMaterial.SetColor("_EmissionColor", Color.black);
            GlowIncreaseAnimation();
            dustParticles.Activate(glowDelay, glowDuration);
            coolDown = true;
        }
    }

    public void NoDashFeedback(Color _NoDashFeedbackColor)
    {
        Debug.Log("No Dash Feedback Playing");
        Color _currentWingEmissionColor = Color.black;
        LeanTween.value(gameObject, 0f, 1f, 0.3f).setOnUpdate((float _colorLerpValue) =>
        {
            _currentWingEmissionColor = Color.Lerp(_NoDashFeedbackColor, currentWingEmissionColor, _colorLerpValue);
            wingMaterial.SetColor("_EmissionColor", _currentWingEmissionColor);
        }).setEaseOutQuad().setOnComplete(NoDashFeedbackAnimationComplete);
        NoDashFeedbackPlaying = true;
    }

    void GlowIncreaseAnimation()
    {
        Debug.Log("Glow Increase Animation Playing");
        Color _currentWingEmissionColor = Color.black;
        LeanTween.value(gameObject, 0f, 1f, glowDuration).setOnUpdate((float _colorLerpValue) =>
        {
            currentWingEmissionColor = Color.Lerp(Color.black, glowColor, _colorLerpValue);
            if (!NoDashFeedbackPlaying)
            {
                wingMaterial.SetColor("_EmissionColor", currentWingEmissionColor);
            }
        }).setEaseInExpo().setDelay(glowDelay);

    }


    void NoDashFeedbackAnimationComplete()
    {
        wingMaterial.SetColor("_EmissionColor", currentWingEmissionColor);
        NoDashFeedbackPlaying = false;
    }

    public void EndCooldown()
    {
        currentWingEmissionColor = Color.black;
        wingMaterial.SetColor("_EmissionsColor", currentWingEmissionColor);
        coolDown = false;
    }
    private void SingletonCheck()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning("Another instance of" + this + " exists, destroying this");
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
