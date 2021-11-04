using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Pickup : MonoBehaviour
{
    public GameObject Door;
    public GameObject Key;



    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (!col.isTrigger)
            {
                Door.SetActive(false);
                Key.SetActive(false);
            }
        }
    }
}
