using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MistFX : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 1f)]
    float strength = 0.5f, density = 0.2f;

    ParticleSystem particleSystem;

    private void OnEnable()
    {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        particleSystem.startColor = new Color(1f,1f,1f, strength);
        particleSystem.maxParticles = Mathf.FloorToInt(density * 1000f);
    }
}
