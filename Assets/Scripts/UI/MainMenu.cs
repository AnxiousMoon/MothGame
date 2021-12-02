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
    [SerializeField] GameObject StartButtonObj, QuitButtonObj;
    Image startButtonImg, quitButtonImg;

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


    public void StartButtonPressed()
    {
        if (onMenu)
        {
            FadeOutMenu();
            onMenu = false;
            StartCoroutine(PostMenuCountdown());

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Start()
    {
        Splash.SetActive(true);

        Color transparentColor = new Color(1f, 1f, 1f, 0f);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        titleImg = title.GetComponent<Image>();
        titleImg.color = transparentColor;

        subtitleImg = subtitle.GetComponent<Image>();
        subtitleImg.color = transparentColor;

        pressToStartTxt = pressToStart.GetComponent<Text>();
        pressToStartTxt.color = new Color(pressToStartTxt.color.r, pressToStartTxt.color.g, pressToStartTxt.color.b, 0f);

        startButtonImg = StartButtonObj.GetComponent<Image>();
        quitButtonImg = QuitButtonObj.GetComponent<Image>();
        startButtonImg.color = new Color(1f, 1f, 1f, 0f);
        quitButtonImg.color = new Color(1f, 1f, 1f, 0f);

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
            startButtonImg.color = _textColor;
            quitButtonImg.color = _textColor;
        }).setEase(leanTweenFadeInType).setDelay(pressToStartDelay);
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
            startButtonImg.color = _color;
            quitButtonImg.color = _color;
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
