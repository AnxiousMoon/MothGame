using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void CloseApplication()
    {
        Application.Quit();
        Debug.LogWarning("Application has quit. It will not take effect in editor mode.");
    }
}
