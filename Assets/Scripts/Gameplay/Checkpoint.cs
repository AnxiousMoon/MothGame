using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject checkPoint;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Checkpoint")
        {
            checkPoint = col.gameObject;
            col.enabled = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Enemy")
        {
            player.transform.position = checkPoint.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
