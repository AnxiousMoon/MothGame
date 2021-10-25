using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject Exposition;
    public GameObject Splash;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Exposition.SetActive(true);
            Splash.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
