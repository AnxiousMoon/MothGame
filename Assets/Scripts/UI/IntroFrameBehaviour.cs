using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroFrameBehaviour : MonoBehaviour
{
    #region parameters
    Image image;
    Text text;

    Exposition exposition;

    float frameDuration;
    [Header("Frame Specific Parameters")]
    [SerializeField] [Tooltip("The time it takes for the frame to fade in in seconds")]float fadeDuration = 1f;
    [SerializeField] [Tooltip("The time between frames in seconds.")] float frameDelay = 1f;
    [SerializeField] [Tooltip("The time it takes for the text to appear after the frame appears in seconds.")] float textDelay = 0.5f;

    bool isActive = false;
    #endregion

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
        text = transform.GetChild(0).GetComponent<Text>();
    }

    private void Start()
    {
        //make frame and text invisible
        textDelay += frameDelay;
        image.color = Color.clear;
        text.color = Color.clear;
    }
    public void Activate(float _frameDuration, Exposition _exposition)
    {
        if (!isActive)
        {
            //Grab a reference to exposition script during activation
            exposition = _exposition;
            frameDuration = _frameDuration;

            //Frame fade in.
            LeanTween.value(gameObject, 0f, 1f, fadeDuration).setOnUpdate((float _alpha) =>
            {
                image.color = new Color(1f, 1f, 1f, _alpha);
            }).setEaseOutQuad().setOnComplete(FadeOut).setDelay(frameDelay);

            //Text Fade in
            LeanTween.value(text.gameObject, 0f, 1f, fadeDuration).setOnUpdate((float _alpha) =>
            {
                text.color = new Color(1f, 1f, 1f, _alpha);
            }).setEaseOutQuad().setDelay(textDelay);

            isActive = true;
        }
    }

    public void Skip()
    {
        LeanTween.cancel(gameObject);
        LeanTween.value(gameObject, 1f, 0f, 0.1f).setOnUpdate((float _alpha) =>
        {
            image.color = new Color(1f, 1f, 1f, _alpha);
            text.color = new Color(1f, 1f, 1f, _alpha);
        }).setEaseInQuad().setOnComplete(FrameComplete);
    }
    void FadeOut()
    {
        //Fade out both text and image
        LeanTween.value(gameObject, 1f, 0f, fadeDuration).setOnUpdate((float _alpha) =>
        {
            image.color = new Color(1f, 1f, 1f, _alpha);
            text.color = new Color(1f, 1f, 1f, _alpha);
        }).setEaseInQuad().setDelay(frameDuration).setOnComplete(FrameComplete);
    }

    //calls the exposition script once animation is complete.
    void FrameComplete()
    {
        
        exposition.NextFrame();
        Destroy(gameObject);
    }
}
