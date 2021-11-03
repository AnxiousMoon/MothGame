using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SceneLightingSettings : MonoBehaviour
{
    private static SceneLightingSettings _instance;
    public static SceneLightingSettings instance { get { return _instance; } }


    [SerializeField] Color coolLightColor = Color.white;
    [SerializeField] Color warmLightColor = Color.white;

    private void OnEnable()
    {
        SingletonCheck();
    }

    public Color GetCoolColor()
    {
        return coolLightColor;
    }

    public Color GetWarmColor()
    {
        return warmLightColor;
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
