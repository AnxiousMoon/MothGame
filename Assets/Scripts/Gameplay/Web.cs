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
        }

        if (col.collider.tag == "Dashing" && gameObject.tag == "Stuck")
        {
            webFX.Play();
            Destroy(stuckBat);
            Destroy(web);
            this.col.enabled = false;
            webBroken.SetActive(true);
        }

        if (col.collider.tag == "Dashing" && gameObject.tag == "Full")
        {
            webFX.Play();
            Destroy(stuckBat);
            Destroy(stuckBat2);
            Destroy(web);
            this.col.enabled = false;
            webBroken.SetActive(true);
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
