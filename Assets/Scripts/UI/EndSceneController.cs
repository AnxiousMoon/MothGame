using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour
{
    [SerializeField] Image doorTeaserImage;
    [SerializeField] Text comingSoonText;
    [SerializeField] float fadeInTime = 1f, fadeOutTime = 1f, duration = 5f, endDelay = 1f;
    
    private void Start()
    {
        StartCoroutine(EndSequence());
        doorTeaserImage.color = new Color(1f, 1f, 1f, 0f);
    }

    IEnumerator EndSequence()
    {
        FadeIn();
        yield return new WaitForSeconds(fadeInTime + duration);
        FadeOut();
        yield return new WaitForSeconds(fadeOutTime + endDelay);
        //Load first scene in build
        SceneManager.LoadScene(0);

    }

    void FadeIn()
    {
        LeanTween.value(doorTeaserImage.gameObject, 0, 1f, fadeInTime).setOnUpdate((float _alpha) =>
        {
            doorTeaserImage.color = new Color(1f, 1f, 1f, _alpha);
            comingSoonText.color = new Color(1f,1f,1f,_alpha);
        }).setEaseInQuad();
    }

    void FadeOut()
    {
        LeanTween.value(doorTeaserImage.gameObject, 1f, 0f, fadeOutTime).setOnUpdate((float _alpha) =>
        {
            doorTeaserImage.color = new Color(1f, 1f, 1f, _alpha);
            comingSoonText.color = new Color(1f,1f,1f,_alpha);
        }).setEaseOutQuad() ;
    }
    
}
