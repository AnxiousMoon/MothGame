using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance { get { return _instance; } }


    [SerializeField] CursorLockMode cursorLockMode = CursorLockMode.Locked;
    [SerializeField] bool hideCursor = true;

    bool[] sceneCutscenePlayed;

    private void Awake()
    {
        SingletonCheck();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        sceneCutscenePlayed = new bool[3];
        for (int i = 0; i < sceneCutscenePlayed.Length; i++)
        {
            sceneCutscenePlayed[i] = false;
        }
        
        Cursor.lockState = cursorLockMode;
        Cursor.visible = !hideCursor;
    }

    public bool GetIsCutscenePlayed(int _scene)
    {
        return sceneCutscenePlayed[_scene];
    }

    public void SetIsCutscenePlayed(int _scene, bool _bool)
    {
        sceneCutscenePlayed[_scene] = _bool;
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
