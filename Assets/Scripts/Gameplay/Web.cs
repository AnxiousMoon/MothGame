using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Web : MonoBehaviour
{
    public GameObject web;
    public GameObject webBroken;
    public GameObject stuckBat;
    public GameObject stuckBat2;
    public Transform stuckBatPos;

    [SerializeField] ParticleSystem webFX;

    Collider col;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Dashing" && gameObject.tag == "Web")
        {
            webFX.Play();
            Destroy(web);
            this.col.enabled = false;
            webBroken.SetActive(true);
            col.collider.tag = "Player";
        }

        if (col.collider.tag == "Dashing" && gameObject.tag == "Stuck")
        {
            webFX.Play();
            //Checks for type of bat then executes the relevant method.
            if (stuckBat.GetComponent<BatAIStationary>())
            {
                stuckBat.GetComponent<BatAIStationary>().Kill();
            }else if (stuckBat.GetComponent<BatAI>())
            {
                stuckBat.GetComponent<BatAI>().Kill();
            }
            else
            {
                Debug.LogError("Could not find a Bat AI component attached to " + stuckBat.name);
            }

            
            this.col.enabled = false;
            webBroken.SetActive(true);
            Destroy(web);
            col.collider.tag = "Player";
        }

            //This runs if there are two bats in the web
        if (col.collider.tag == "Dashing" && gameObject.tag == "Full")
        {
            webFX.Play();

                //Checks for type of bat1 then executes the relevant method.
            if (stuckBat.GetComponent<BatAIStationary>())
            {
                stuckBat.GetComponent<BatAIStationary>().Kill();
            }
            else if (stuckBat.GetComponent<BatAI>())
            {
                stuckBat.GetComponent<BatAI>().Kill();
            }
            else
            {
                Debug.LogError("Could not find a Bat AI component attached to " + stuckBat.name);
            }

                //Repeats the same code, but checks for bat2
            if (stuckBat2.GetComponent<BatAIStationary>())
            {
                stuckBat2.GetComponent<BatAIStationary>().Kill();
            }
            else if (stuckBat2.GetComponent<BatAI>())
            {
                stuckBat2.GetComponent<BatAI>().Kill();
            }
            else
            {
                Debug.LogError("Could not find a Bat AI component attached to " + stuckBat.name);
            }

            this.col.enabled = false;
            webBroken.SetActive(true);
            Destroy(web);
            col.collider.tag = "Player";
        }

        if (col.collider.tag == "Enemy" && gameObject.tag == "Web")
        {
            stuckBat = col.gameObject;
            gameObject.tag = "Stuck";
            stuckBat.tag = "Stuck";
        }

        if (col.collider.tag == "Enemy" && gameObject.tag == "Stuck")
        {
            gameObject.tag = "Full";
            stuckBat.transform.position = stuckBatPos.position;
            stuckBat2 = col.gameObject;
        }
    }
}
