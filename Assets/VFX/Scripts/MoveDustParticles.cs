using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDustParticles : MonoBehaviour
{
    float velocityMag;
    Vector3 zxPosLastFrame, zxPosThisFrame;
    ParticleSystem dustParticleSystem;
    ParticleSystem.EmissionModule emissionModule;
    float initialRateOverTime = 0f;
    [SerializeField] float maximumSpeed = 3f;

    private void Awake()
    {
        dustParticleSystem = gameObject.GetComponent<ParticleSystem>();
        emissionModule = dustParticleSystem.emission;
        
    }

    private void Start()
    {
        initialRateOverTime = emissionModule.rateOverTime.constant;
    }

    private void FixedUpdate()
    {
        zxPosThisFrame = transform.position;

        float distanceTravelled = Vector3.Distance(zxPosLastFrame, zxPosThisFrame);
        velocityMag = distanceTravelled / Time.fixedDeltaTime;
        if(Mathf.Clamp(velocityMag, 0, maximumSpeed) / maximumSpeed > 0.1f)
        {
            dustParticleSystem.Play();
            float emissionRate = Mathf.Clamp(velocityMag, 0, maximumSpeed)/maximumSpeed;
            emissionModule.rateOverTime = Mathf.Pow(emissionRate,2.8f) * initialRateOverTime;
            Debug.Log(emissionRate);
        }
        else
        {
            dustParticleSystem.Stop();
        }
        
        zxPosLastFrame = transform.position;
    }
}
