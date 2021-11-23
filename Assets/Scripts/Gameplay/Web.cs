using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Web : MonoBehaviour
{
    public GameObject web;
    public GameObject webBroken;
    public GameObject stuckBat;
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
        else if (col.collider.tag == "Dashing" && gameObject.tag == "Full")
        {
            webFX.Play();
            Destroy(stuckBat);
            Destroy(web);
            this.col.enabled = false;
            webBroken.SetActive(true);
        }
        if (col.collider.tag == "Enemy")
        {
            stuckBat = col.gameObject;
            gameObject.tag = "Full";
        }
    }
}
