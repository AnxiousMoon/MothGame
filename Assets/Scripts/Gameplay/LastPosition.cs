using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPosition : MonoBehaviour
{
    public Transform currentPosition;
    private Transform oldPosition;
    public GameObject ghost;
    private GameObject newGhost;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("x"))
        {
                if (count >= 1)
                {
                    Destroy(newGhost, 1.0f);
                    count = 0;
                }
                newGhost = (GameObject)Instantiate(ghost, currentPosition.position, currentPosition.rotation);
                count++;
        }
    }
}
