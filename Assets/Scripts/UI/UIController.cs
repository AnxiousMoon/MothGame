using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    UIController _instance;
    UIController instance { get { return _instance; } }

    [SerializeField] FadePanel fadePanel;
    [SerializeField] DeathCircleUI deathCircle;

    private void Awake()
    {
        SingletonCheck();
    }

    private void Start()
    {
        SceneFadeIn();
        DeathCircle();
    }

    public void SceneFadeIn()
    {
        fadePanel.FadeOut();
    }

    public void DeathCircle()
    {
        deathCircle.Activate();
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
