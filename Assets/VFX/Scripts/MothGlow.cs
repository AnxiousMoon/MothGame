using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothGlow : MonoBehaviour
{
    //Singleton
    private static MothGlow _instance;
    public static MothGlow instance { get { return _instance; } }


    [SerializeField][ColorUsage(true,true)] 
    Color glowColor = new Color(63f / 255f, 220f / 255f, 203f / 255f, 1f);
    [SerializeField]
    [ColorUsage(true, true)] Color negativeDashColor;
    [SerializeField]
    float glowDuration = 2f;
    float glowDelay = 8f;
    [SerializeField]
    DustParticles dustParticles;
    Material wingMaterial;
    Color currentWingEmissionColor = Color.black;
    float colorLerpValue = 0f;

    float cooldownDuration;
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


    public void ClickDash(float _cooldown)
    {
        _cooldown -= 0.25f;
        cooldownDuration = _cooldown;
        glowDelay = _cooldown * 0.8f;
        glowDuration = _cooldown * 0.2f;

        currentWingEmissionColor = Color.black;
        wingMaterial.SetColor("_EmissionColor", currentWingEmissionColor);
        
        GlowIncreaseAnimation();
        dustParticles.Activate(glowDelay, glowDuration);
        coolDown = true;

    }


    private void Update()
    {
        cooldownDuration -= Time.deltaTime;
        if(cooldownDuration <= 0)
        {
            coolDown = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && coolDown)
        {
            NoDashFeedback();
        }
    }

    public void NoDashFeedback()
    {
        Debug.Log("No Dash Feedback Playing");
        Color _currentWingEmissionColor = Color.black;
        LeanTween.value(gameObject, 0f, 1f, 0.3f).setOnUpdate((float _colorLerpValue) =>
        {
            _currentWingEmissionColor = Color.Lerp(negativeDashColor, currentWingEmissionColor, _colorLerpValue);
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
