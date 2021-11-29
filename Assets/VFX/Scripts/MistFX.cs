using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MistFX : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 1f)]
    [Tooltip("The opacity of the cloud")] float strength = 0.5f;

    [SerializeField]
    [Range(0f, 1f)]
    [Tooltip("The maximum number of particles")] float density = 0.2f;

    ParticleSystem _particleSystem;

    private void OnEnable()
    {
        _particleSystem = gameObject.GetComponent<ParticleSystem>();
        _particleSystem.startColor = new Color(1f,1f,1f, strength);
        _particleSystem.maxParticles = Mathf.FloorToInt(density * 1000f);
    }
}
