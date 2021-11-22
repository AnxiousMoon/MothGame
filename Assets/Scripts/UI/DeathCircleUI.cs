using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCircleUI : MonoBehaviour
{
    [SerializeField] float targetScale = 100f;
    [SerializeField] Transform playerChest;
    Camera mainCamera;
    [SerializeField] [Range(0f,1f)] float initialDelay = 0.25f, scaleDuration = 0.3f, intermediateScaleDelay = 0.2f, secondScaleDuration = 0.25f;
    RectTransform rectTransform;
    
    float scale;
    float startingScale;
    float respawnDelay = 0f;

    bool isActive = false;
    private void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        startingScale = rectTransform.sizeDelta.x;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }
    public void Activate(float _respawnDelay)
    {
        if (!isActive)
        {
            respawnDelay = _respawnDelay;
            initialDelay *= _respawnDelay;
            scaleDuration *= _respawnDelay;
            intermediateScaleDelay *= _respawnDelay;
            secondScaleDuration *= _respawnDelay;

            Debug.Log(scaleDuration);

            LeanTween.value(gameObject, startingScale, targetScale, scaleDuration).setOnUpdate((float _scale) =>
           {
               rectTransform.position = mainCamera.WorldToScreenPoint(playerChest.position);
               scale = _scale;
               rectTransform.sizeDelta = new Vector2(scale, scale);
           }).setEaseOutQuad().setDelay(initialDelay).setOnComplete(FadeOut);

            isActive = true;
        }
    }

    void FadeOut()
    {
        FadePanel.instance.FadeIn(secondScaleDuration);
    }

    void ScaleToZero()
    {
        LeanTween.value(gameObject, scale, 0, secondScaleDuration).setOnUpdate((float _scale) =>
        {
            rectTransform.position = mainCamera.WorldToScreenPoint(playerChest.position);
            scale = _scale;
            rectTransform.sizeDelta = new Vector2(scale, scale);
        }).setEaseOutQuad().setDelay(intermediateScaleDelay);
    }
}
