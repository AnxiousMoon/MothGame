using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour
{
    [SerializeField] LeanTweenType leanTweenType;
    [SerializeField] float defaultFadeDuration = 0.5f;
    Image panel;
    bool fadedOut = false;

    private void Awake()
    {
        panel = gameObject.GetComponent<Image>();
    }

    public void FadeOut()
    {
        if (!fadedOut)
        {
            LeanTween.value(gameObject, 1f, 0f, defaultFadeDuration).setOnUpdate((float _alpha) =>
            {
                Color _color = new Color(0f, 0f, 0f, _alpha);
                panel.color = _color;

            }).setEaseOutCubic().setDelay(0.5f).setOnComplete(FadedOut);
        }
    }

    void FadedOut()
    {
        fadedOut = true;
    }

    public void FadeIn()
    {
        if (!fadedOut)
        {
            LeanTween.value(gameObject, 0f, 1f, defaultFadeDuration).setOnUpdate((float _alpha) =>
            {
                Color _color = new Color(0f, 0f, 0f, _alpha);
                panel.color = _color;

            }).setEaseOutCubic().setDelay(0.5f).setOnComplete(FadedOut);
        }
    }

    void FadedIn()
    {
        fadedOut = false;
    }


}
