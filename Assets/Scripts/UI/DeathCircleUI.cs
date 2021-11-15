using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCircleUI : MonoBehaviour
{
    [SerializeField] float targetScale = 100f;
    [SerializeField] [Range(0f,1f)] float initialDelay = 0.25f, scaleDuration = 0.3f, intermediateScaleDelay = 0.2f, secondScaleDuration = 0.25f;
    RectTransform rectTransform;
    
    float scale;
    float startingScale;
    float respawnDelay = 0f;

    private void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        startingScale = rectTransform.sizeDelta.x;
    }
    public void Activate(float _respawnDelay)
    {
        respawnDelay = _respawnDelay;
        initialDelay *= _respawnDelay;
        scaleDuration *= _respawnDelay;
        intermediateScaleDelay *= _respawnDelay;
        secondScaleDuration *= _respawnDelay;

        Debug.Log(scaleDuration);

        LeanTween.value(gameObject, startingScale, targetScale, scaleDuration).setOnUpdate((float _scale) =>
       {
           scale = _scale;
           rectTransform.sizeDelta = new Vector2(scale, scale);
       }).setEaseOutQuad().setDelay(initialDelay).setOnComplete(ScaleToZero);

    }

    void ScaleToZero()
    {
        LeanTween.value(gameObject, scale, 0, secondScaleDuration).setOnUpdate((float _scale) =>
        {
            scale = _scale;
            rectTransform.sizeDelta = new Vector2(scale, scale);
        }).setEaseOutQuad().setDelay(intermediateScaleDelay);
    }
}
