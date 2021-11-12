using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost : MonoBehaviour
{
    public Collider collider;
    public Collider ghostBox;
    public Transform returnPoint;
    public Transform fix;
    bool reset = true;
    private float x, y, z, xR, yR, zR;
    Vector3 fixRotation;


    // Start is called before the first frame update
    void Start()
    {
        fixRotation = collider.transform.eulerAngles;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            ghostBox.enabled = false;
            collider.enabled = false;
        }
        if (col.tag == "Web")
        {
            ghostBox.transform.position = col.transform.position;
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Web")
        {
            ghostBox.transform.position = col.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (collider.enabled)
        {
            transform.position = new Vector3(x, y, z);
            collider.transform.position = new Vector3(x, y, z);
            collider.transform.rotation = Quaternion.Euler(xR, yR, zR);
            ghostBox.enabled = true;
            reset = false;
        }
        else if (!collider.enabled)
        {
            x = transform.position.x;
            y = transform.position.y;
            z = transform.position.z;
            xR = transform.rotation.x;
            yR = transform.rotation.y;
            zR = transform.rotation.z;
            if (!reset)
            {
                ghostBox.enabled = false;
                transform.position = returnPoint.position;
                collider.transform.localRotation = Quaternion.Euler(0, 45, 0);
                reset = true;
            }
        }
    }
}
