using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    //public GameObject checkPoint;
    //public GameObject player;
    // Start is called before the first frame update
    [SerializeField] Move playerMoveScript;
    [SerializeField] float respawnDelay = 2f;
    [SerializeField] DeathFX playerDeathParticleControl;
    UIController uiController;
    MothAnimation mothAnimation;

    //makes sure checkpoint reset can only be called once;
    bool checkpointResetting = false;
    void Start()
    {
        uiController = UIController.instance;
        mothAnimation = MothAnimation.instance;
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
        if (col.collider.tag == "Enemy" && !checkpointResetting)
        {
            //player.transform.position = checkPoint.transform.position;
            //MothAnimation.instance.ResetRotation();
            //FadePanel.instance.FadeOut();
            checkpointResetting = true;
            playerDeathParticleControl.Activate();
            mothAnimation.PlayDeath();
            playerMoveScript.AllowPlayerMovement(false);
            StartCoroutine(Respawn());
        }
        if (col.collider.tag == "Web" && !checkpointResetting)
        {
            if (gameObject.tag != "Dashing")
            {
                //player.transform.position = checkPoint.transform.position;
                //MothAnimation.instance.ResetRotation();
                //FadePanel.instance.FadeOut();
                
                /*  Web kill player script
                checkpointResetting = true;
                playerDeathParticleControl.Activate();
                mothAnimation.PlayDeath();
                playerMoveScript.AllowPlayerMovement(false);
                StartCoroutine(Respawn());
                */
            }
        }
        if (col.collider.tag == "End")
        {
            //FadePanel.instance.FadeOut();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Death" && !checkpointResetting)
        {
            checkpointResetting = true;
            playerDeathParticleControl.Activate();
            mothAnimation.PlayDeath();
            playerMoveScript.AllowPlayerMovement(false);
            StartCoroutine(Respawn());
        }
    }



    IEnumerator Respawn()
    {
        uiController.DeathCircle(respawnDelay);
        yield return new WaitForSeconds(respawnDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
