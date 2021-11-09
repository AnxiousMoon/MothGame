using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Pickup : MonoBehaviour
{
    public GameObject Door;
    public GameObject Key;

    public AK.Wwise.Event Sound;

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
                    Sound.Post(gameObject);
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
