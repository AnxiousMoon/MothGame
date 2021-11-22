using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exposition : MonoBehaviour
{
    IntroFrameBehaviour[] frames;
    [SerializeField] [Tooltip("Controls how long each frame is visible before moving on to the next.")] float frameDuration = 5f;
    int currentFrame = 0;
    MainMenu mainMenu;

    private void Awake()
    {
        //create a new frames array and attach each element to this transform's children.
        frames = new IntroFrameBehaviour[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            frames[i] = transform.GetChild(i).GetComponent<IntroFrameBehaviour>();
        }
    }

    //Called by MainMenu.cs
    public void Activate(MainMenu _mainMenu)
    {
        mainMenu = _mainMenu;
        frames[currentFrame].Activate(frameDuration, this);
    }

    //Activates the current frame, until the limit is reached where the next scene will be started
    public void NextFrame()
    {
        currentFrame++;
        if (currentFrame < frames.Length)
        {
            frames[currentFrame].Activate(frameDuration, this);
        }
        else
        {
            mainMenu.StartGame();
        }
    }
}
