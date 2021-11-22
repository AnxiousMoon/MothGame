using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    bool pauseActive = false;
    Image canvasImg;
    [SerializeField] GameObject QuitButtonObj, RetryButtonObj, pauseTitle;
    Button retryButton;
    Button quitButton;

    private void Awake()
    {
        canvasImg = gameObject.GetComponent<Image>();

    }

    private void Start()
    {
        quitButton = QuitButtonObj.GetComponent<Button>();
        QuitButtonObj.SetActive(false);
        RetryButtonObj.SetActive(false);
        pauseTitle.SetActive(false);
        canvasImg.enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !pauseActive)
        {
            canvasImg.enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            QuitButtonObj.SetActive(true);
            RetryButtonObj.SetActive(true);
            pauseTitle.SetActive(true);
            Time.timeScale = 0;

            pauseActive = true;
        }else if(Input.GetKeyUp(KeyCode.Escape) && pauseActive)
        {
            canvasImg.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            QuitButtonObj.SetActive(false);
            RetryButtonObj.SetActive(false);
            pauseTitle.SetActive(false);
            Time.timeScale = 1;

            pauseActive = false;
        }
    }
}
