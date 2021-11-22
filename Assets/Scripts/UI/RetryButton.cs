using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void ResetLevel()
    {
        Time.timeScale = 1f;
        LeanTween.cancelAll();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
        Application.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
