using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    //public GameObject checkPoint;
    //public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    //void OnTriggerEnter(Collider col)
    //{
    //    if (col.tag == "Checkpoint")
    //    {
    //        checkPoint = col.gameObject;
    //        col.enabled = false;
    //    }
    //    //if (col.tag == "Web")
    //    //{
    //    //    player.transform.position = checkPoint.transform.position;
    //    //    MothAnimation.instance.ResetRotation();
    //    //}
    //}

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Enemy")
        {
            //player.transform.position = checkPoint.transform.position;
            //MothAnimation.instance.ResetRotation();
            //FadePanel.instance.FadeOut();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (col.collider.tag == "Web")
        {
            if (gameObject.tag != "Dashing")
            {
                //player.transform.position = checkPoint.transform.position;
                //MothAnimation.instance.ResetRotation();
                //FadePanel.instance.FadeOut();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (col.collider.tag == "End")
        {
            //FadePanel.instance.FadeOut();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
