using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private static UIController _instance;
    public static UIController instance { get { return _instance; } }

    [SerializeField] FadePanel fadePanel;
    [SerializeField] DeathCircleUI deathCircle;

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

    public void DeathCircle(float _respawnDelay)
    {
        deathCircle.Activate(_respawnDelay);
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
