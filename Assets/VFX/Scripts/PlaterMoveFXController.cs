using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaterMoveFXController : MonoBehaviour
{
    [Header("Pebbles")]
    [SerializeField] ParticleSystem RightFootPebbleEmitter;
    [SerializeField] ParticleSystem LeftFootPebbleEmitter;

    [SerializeField] ParticleSystem Dust;
    public void RightFoot()
    {
        RightFootPebbleEmitter.Play();
        EmitDust();
    }

    public void LeftFoot()
    {
        LeftFootPebbleEmitter.Play();
        EmitDust();
    }

    void EmitDust()
    {
        //Dust.Play();
    }
}
