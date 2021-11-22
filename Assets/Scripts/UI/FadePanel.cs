using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour
{
    private static FadePanel _instance;
    public static FadePanel instance { get { return _instance; } }

    [SerializeField] LeanTweenType leanTweenType;
    [SerializeField] float defaultFadeDuration = 0.5f;
    Image panel;
    bool fadedOut = false;

    private void Awake()
    {
        
        SingletonCheck();
        panel = gameObject.GetComponent<Image>();
    }

    private void Start()
    {
        fadedOut = false;
        
        Debug.Log(panel);
        //FadeOut();
        StartCoroutine(FadeOutDelay());
    }

    IEnumerator FadeOutDelay()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("FadeOut Delay is running");
        FadeOut();
    }

    public void FadeOut()
    {
        Debug.Log("Fade Out is running");
        if (!fadedOut)
        {
            LeanTween.value(gameObject, 1f, 0f, defaultFadeDuration).setOnUpdate((float _alpha) =>
            {
                Color _color = new Color(0f, 0f, 0f, _alpha);
                panel.color = _color;

            }).setEaseOutCubic().setDelay(0.5f).setOnComplete(FadedOut);
        }
    }

    public void FadeOut(float _duration)
    {
        if (!fadedOut)
        {
            LeanTween.value(gameObject, 1f, 0f, _duration).setOnUpdate((float _alpha) =>
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
        if (fadedOut)
        {
            LeanTween.value(gameObject, 0f, 1f, defaultFadeDuration).setOnUpdate((float _alpha) =>
            {
                Color _color = new Color(0f, 0f, 0f, _alpha);
                panel.color = _color;

            }).setEaseOutCubic().setDelay(0.5f).setOnComplete(FadedIn);
        }
    }

    public void FadeIn(float _duration)
    {
        Debug.Log("Fading In");
        if (fadedOut)
        {
            LeanTween.value(gameObject, 0f, 1f, _duration).setOnUpdate((float _alpha) =>
            {
                Color _color = new Color(0f, 0f, 0f, _alpha);
                panel.color = _color;

            }).setEaseOutCubic().setDelay(0.5f).setOnComplete(FadedIn);
        }
    }

    void FadedIn()
    {
        fadedOut = false;
    }

    private void SingletonCheck()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning("Another instance of" + this + " exists, destroying this");
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

}
