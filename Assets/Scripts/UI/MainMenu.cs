using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject Exposition;
    public GameObject Splash;
    [SerializeField] GameObject title, subtitle, pressToStart, story;
    Image titleImg, subtitleImg;
    Text pressToStartTxt, storyTxt;
    [SerializeField]
    float titleFadeInDuration = 1f,
        subtitleDelay = 0.5f,
        pressToStartDelay = 1f,
        expostitionDelay = 1f,
        expositionDuration = 10f;
        
    
    [SerializeField] LeanTweenType leanTweenFadeInType, leanTweenFadeOutType;

    bool onMenu = true;
    bool skipExposition = false;
    bool postMenuCountdown = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            if (onMenu)
            {
                FadeOutMenu();
                onMenu = false;
                StartCoroutine(PostMenuCountdown());
            }
            else if(!skipExposition && postMenuCountdown)
            {
                Debug.Log("Skip Exposition");
                SkipExposition();
                skipExposition = true;
            }
        }
    }

    private void Start()
    {
        Splash.SetActive(true);
        Exposition.SetActive(false);

        Color transparentColor = new Color(1f, 1f, 1f, 0f);

        titleImg = title.GetComponent<Image>();
        titleImg.color = transparentColor;

        subtitleImg = subtitle.GetComponent<Image>();
        subtitleImg.color = transparentColor;

        pressToStartTxt = pressToStart.GetComponent<Text>();
        pressToStartTxt.color = new Color(pressToStartTxt.color.r, pressToStartTxt.color.g, pressToStartTxt.color.b, 0f);

        storyTxt = story.GetComponent<Text>();
        storyTxt.color = transparentColor;

        TitleFadeIn();
    }

    void TitleFadeIn()
    {
        LeanTween.value(title, 0f, 1f, titleFadeInDuration).setOnUpdate((float _alpha) =>
        {
            Color _titleColor = new Color(1f, 1f, 1f, _alpha);
            titleImg.color = _titleColor;
        }).setEase(leanTweenFadeInType).setDelay(1f).setOnComplete(SubtitleFadeIn);

    }

    void SubtitleFadeIn()
    {
        LeanTween.value(subtitle, 0f, 1f, titleFadeInDuration).setOnUpdate((float _alpha) =>
        {
            Color _subtitleColor = new Color(1f, 1f, 1f, _alpha);
            subtitleImg.color = _subtitleColor;
        }).setEase(leanTweenFadeInType).setDelay(subtitleDelay).setOnComplete(PressToStartFadeIn);
    }

    void PressToStartFadeIn()
    {
        LeanTween.value(pressToStart, 0f, 1f, titleFadeInDuration).setOnUpdate((float _alpha) =>
        {
            Color _textColor = new Color(pressToStartTxt.color.r, pressToStartTxt.color.g, pressToStartTxt.color.b, _alpha);
            pressToStartTxt.color = _textColor;
        }).setEaseInOutCirc().setLoopPingPong().setDelay(pressToStartDelay);
    }

    void FadeOutMenu()
    {
        LeanTween.cancelAll();
        LeanTween.value(gameObject, 1f, 0f, titleFadeInDuration).setOnUpdate((float _alpha) =>
        {
            Color _color = new Color(1f, 1f, 1f, _alpha);
            titleImg.color = _color;
            pressToStartTxt.color = _color;
            subtitleImg.color = _color;
        }).setEase(leanTweenFadeOutType).setDelay(subtitleDelay).setOnComplete(OpenExposition);
    }

    void OpenExposition()
    {
        Exposition.SetActive(true);
        Splash.SetActive(false);

        LeanTween.value(story, 0f, 1f, titleFadeInDuration).setOnUpdate((float _alpha) =>
        {
            Color _color = new Color(1f, 1f, 1f, _alpha);
            storyTxt.color = _color;
        }).setEase(leanTweenFadeInType).setDelay(expostitionDelay).setOnComplete(FadeOutExposition);
    }

    void FadeOutExposition()
    {
        Debug.Log("Fade out exposition");
        LeanTween.value(story, 1f, 0f, titleFadeInDuration).setOnUpdate((float _alpha) =>
        {
            Color _color = new Color(1f, 1f, 1f, _alpha);
            storyTxt.color = _color;
        }).setEase(leanTweenFadeOutType).setDelay(expositionDuration).setOnComplete(StartGame);
    }

    void SkipExposition()
    {
        LeanTween.cancel(story);
        StartCoroutine(SkipExpositionDelay());
    }

    IEnumerator SkipExpositionDelay()
    {
        yield return new WaitForSeconds(0.1f);
        expositionDuration = 0f;
        FadeOutExposition();
    }

    IEnumerator PostMenuCountdown()
    {
        yield return new WaitForSeconds(2f);
        postMenuCountdown = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
