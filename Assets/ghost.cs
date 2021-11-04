using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost : MonoBehaviour
{
    public Collider collider;
    public Collider ghostBox;
    public Transform returnPoint;
    bool reset = true;
    private float x, y, z;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (collider.enabled)
        {
            transform.position = new Vector3(x, y, z);
            ghostBox.enabled = true;
            reset = false;
        }
        else if (!collider.enabled)
        {
            x = transform.position.x;
            y = transform.position.y;
            z = transform.position.z;
            if (!reset)
            {
                ghostBox.enabled = false;
                transform.position = returnPoint.position;
                reset = true;
            }    
        }
    }
}
