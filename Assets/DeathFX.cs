using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFX : MonoBehaviour
{
    ParticleSystem deathEffect;

    private void Awake()
    {
        deathEffect = gameObject.GetComponent<ParticleSystem>();

    }

    public void Activate()
    {
        deathEffect.Play();
    }
}
