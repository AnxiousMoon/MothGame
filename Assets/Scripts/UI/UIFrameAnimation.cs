using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFrameAnimation : MonoBehaviour
{
    [SerializeField] Texture frame00, frame01;
    [SerializeField] float frameRate = 4.0f;
    [SerializeField] float fadeTime = 1.0f;
    Texture[] textures;
    float currentAlpha = 0f;
    Material material;

    bool playerInTrigger = false;
    private void Awake()
    {
        material = gameObject.GetComponent<MeshRenderer>().material;

    }

    private void Start()
    {
        textures = new Texture[2];
        textures[0] = frame00;
        textures[1] = frame01;

        

        if(gameObject.name == "WASD_Move")
        {
            playerInTrigger = true;
            StartCoroutine(FrameAnimation());
            currentAlpha = 1;
        }
        else
        {
            material.SetColor("_BaseColor", new Color(1f,1f,1f,0f));
        }
    }

    public void TriggerEnter()
    {
        if (!playerInTrigger) 
        {
            playerInTrigger = true;
            StartCoroutine(FrameAnimation());
            LeanTween.cancel(gameObject);
            FadeIn();
        }
    }

    public void TriggerExit()
    {
        if (playerInTrigger) 
        { 
            playerInTrigger = false;
            StopCoroutine(FrameAnimation());
            LeanTween.cancel(gameObject);
            FadeOut();
        }
    }

    void FadeOut()
    {
        LeanTween.value(gameObject, currentAlpha, 0f, fadeTime).setOnUpdate((float _currentAlpha) =>
        {
            currentAlpha = _currentAlpha;
            Color currentColor = new Color(1f, 1f, 1f, currentAlpha);
            material.SetColor("_BaseColor", currentColor);
        }).setEaseInCubic();
    }

    void FadeIn()
    {
        LeanTween.value(gameObject, currentAlpha, 1f, fadeTime).setOnUpdate((float _currentAlpha) =>
        {
            currentAlpha = _currentAlpha;
            Color currentColor = new Color(1f, 1f, 1f, currentAlpha);
            material.SetColor("_BaseColor", currentColor);
        }).setEaseOutCubic();
    }

    int currentFrame = 0;
    IEnumerator FrameAnimation()
    {
        while (playerInTrigger)
        {
            material.SetTexture("_BaseMap", textures[currentFrame]);
            currentFrame++;
            currentFrame %= 2;
            yield return new WaitForSeconds(1.0f / frameRate);
        }
    }
}
