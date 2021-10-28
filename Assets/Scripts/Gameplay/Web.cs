using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Web : MonoBehaviour
{
    public GameObject web;
    
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

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Dashing")
        {
            if (!other.isTrigger)
            {
                Destroy(web);
                col.enabled = false;
            }
        }
        else if (other.gameObject.tag == "Player")
        {
            if (!other.isTrigger)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

        }
    }

    void OnCollisionExit(Collision col)
    {
    }
}
