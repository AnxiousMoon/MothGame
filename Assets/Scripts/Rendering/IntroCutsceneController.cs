using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


public class IntroCutsceneController : MonoBehaviour
{

    [SerializeField] PlayableDirector director;
    GameManager gameManager;
    Scene currentScene;
    [SerializeField] Move playerMoveScript;


    
    void Awake()
    {
        director.played += Director_Played;
        director.stopped += Director_Stopped;
        gameManager = GameManager.instance;
        currentScene = SceneManager.GetActiveScene();
    }

    private void Start()
    {
        if (gameManager)
        {
            
            if (!gameManager.GetIsCutscenePlayed(currentScene.buildIndex - 1))
            {
                director.Play();
                playerMoveScript.AllowPlayerMovement(false);
            }
        }
        else
        {
            Debug.LogWarning("There is no Game Manager in this scene. To see the start cutscene, launch the game from the main menu");
        }

    }

    void Director_Played(PlayableDirector obj)
    {
        gameManager.SetIsCutscenePlayed(currentScene.buildIndex - 1, true);
        if (gameManager)
        {
            Debug.Log("Director played");
        }
    }

    void Director_Stopped(PlayableDirector obj)
    {
       
    }

    void StartTimeLine()
    {
        director.Play();
    }

}
