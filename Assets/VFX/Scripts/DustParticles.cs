using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticles : MonoBehaviour
{
    ParticleSystem particleSystem;
    ParticleSystemRenderer particleSystemRenderer;
    Material dustMaterial;
    [SerializeField] [ColorUsage(true, true)] Color glowColor = new Color(63f / 255f, 220f / 255f, 203f / 255f, 1f);
    Color currentColor;
    Color idleColor;

    float glowDelay, glowDuration;

    

    private void Awake()
    {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        particleSystemRenderer= gameObject.GetComponent<ParticleSystemRenderer>();
    }
    private void Start()
    {
        dustMaterial = particleSystemRenderer.material;
        idleColor = particleSystem.startColor;
        currentColor = glowColor;
        dustMaterial.color = glowColor;
    }

    public void Activate(float _glowDelay, float _glowDuration)
    {
        dustMaterial.color = idleColor;
        glowDelay = _glowDelay;
        glowDuration = _glowDuration;
        GlowIncreaseAnimation();
    }
    void GlowIncreaseAnimation()
    {
        LeanTween.value(gameObject, 0f, 1f, glowDuration).setOnUpdate((colorLerpValue) =>
        {
            currentColor = Color.Lerp(Color.black, glowColor, colorLerpValue);
            dustMaterial.color = currentColor;
        }).setEaseInExpo().setDelay(glowDelay);
    }
}
