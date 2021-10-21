using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Pickup : MonoBehaviour
{
    public GameObject Door;
    public GameObject Key;



    private void OnTriggerEnter(Collider other)
    {
        Door.SetActive(false);
        Key.SetActive(false);
    }
}
