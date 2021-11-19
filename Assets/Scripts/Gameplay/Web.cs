using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Web : MonoBehaviour
{
    public GameObject web;
    public GameObject webBroken;
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
        if (col.gameObject.tag == "Dashing")
        {
            webFX.Play();
            Destroy(web);
            this.col.enabled = false;
            webBroken.SetActive(true);
        }
    }
}
