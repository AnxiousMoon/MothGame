using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region Parameters
    [Header("Primary Start Screen Objects")]
    [SerializeField] GameObject Splash;
    [SerializeField] Exposition exposition;

    [Header("Splash References")]
    [SerializeField] GameObject title;
    [SerializeField] GameObject subtitle, pressToStart;

    Image titleImg, subtitleImg;
    Text pressToStartTxt;

    [Header("Splash Animation Properties")]
    [SerializeField] float titleFadeInDuration = 1f;
    [SerializeField] float subtitleDelay = 0.5f,
        pressToStartDelay = 1f;
    
    [SerializeField] LeanTweenType leanTweenFadeInType, leanTweenFadeOutType;

    bool onMenu = true;
    bool postMenuCountdown = false;
    #endregion

    void Update()
    {
        //check for key input, then close menu and start a countdown to move to splash screen
        if (Input.anyKey)
        {
            if (onMenu)
            {
                FadeOutMenu();
                onMenu = false;
                StartCoroutine(PostMenuCountdown());
            }
        }
    }

    private void Start()
    {
        Splash.SetActive(true);

        Color transparentColor = new Color(1f, 1f, 1f, 0f);

        titleImg = title.GetComponent<Image>();
        titleImg.color = transparentColor;

        subtitleImg = subtitle.GetComponent<Image>();
        subtitleImg.color = transparentColor;

        pressToStartTxt = pressToStart.GetComponent<Text>();
        pressToStartTxt.color = new Color(pressToStartTxt.color.r, pressToStartTxt.color.g, pressToStartTxt.color.b, 0f);

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
        exposition.Activate(this);
        Splash.SetActive(false);
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
