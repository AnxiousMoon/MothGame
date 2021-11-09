using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Pickup : MonoBehaviour
{
    public GameObject Door;
    public GameObject Key;

    [SerializeField] float dissoveTime = 1f;

    Dissolve dissolve;

    bool isDissolved = false;

    private void Awake()
    {
        dissolve = gameObject.GetComponent<Dissolve>();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (!col.isTrigger)
            {
                Door.SetActive(false);
                if (!isDissolved)
                {
                    Dissolve();
                }
            }
        }
    }

    void Dissolve()
    {
        
        dissolve.DissolveMe(dissoveTime);
        isDissolved = true;
    }
}
