using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveFXController : MonoBehaviour
{
    [Header("Pebbles")]
    [SerializeField] ParticleSystem RightFootPebbleEmitter;
    [SerializeField] ParticleSystem LeftFootPebbleEmitter;

    [SerializeField] ParticleSystem Dust;
    public void RightFoot()
    {
        RightFootPebbleEmitter.Play();
    }

    public void LeftFoot()
    {
        LeftFootPebbleEmitter.Play();;
    }

    public void PlayDust()
    {
        Dust.Play();
    }

    public void StopDust()
    {
        Dust.Stop();
    }
}
