using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameManager _instance;
    public GameManager instance { get { return _instance; } }


    [SerializeField] CursorLockMode cursorLockMode = CursorLockMode.Locked;
    [SerializeField] bool hideCursor = true;
    private void Awake()
    {
        SingletonCheck();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Cursor.lockState = cursorLockMode;
        Cursor.visible = !hideCursor;
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
