using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem_manager : MonoBehaviour
{
    public GameObject UIOne;
    public GameObject UITwo;
    public GameObject UIThree;

    public int Count;
    private void Update()
    {
        if (Count == 1)
        {
            UIOne.SetActive(true);
        }

        if (Count == 2)
        {
            UITwo.SetActive(true);
            UIOne.SetActive(false);
        }

        if (Count == 3)
        {
            UIThree.SetActive(true);
            UITwo.SetActive(false);
        }
    }
}
