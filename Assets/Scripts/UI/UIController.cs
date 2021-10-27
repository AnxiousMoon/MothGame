using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    UIController _instance;
    UIController instance { get { return _instance; } }

    [SerializeField] FadePanel fadePanel;

    private void Awake()
    {
        SingletonCheck();
    }

    private void Start()
    {
        SceneFadeIn();
    }

    public void SceneFadeIn()
    {
        fadePanel.FadeOut();
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
