using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public Collider collider;
    public float initialTimeOut = 2f;
    private float timeOut;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (!col.isTrigger)
            {
                collider.enabled = true;
            }

        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            collider.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeOut -= Time.deltaTime;
        if (timeOut <= 0f)
        {
            timeOut = initialTimeOut;
            collider.enabled = false;
        }
    }
}
