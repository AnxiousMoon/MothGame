using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Pickup : MonoBehaviour
{
    [SerializeField] Door door;
    public AK.Wwise.Event Sound;

    [SerializeField] float dissolveTime = 1f;

    Dissolve dissolve;

    bool isDissolved = false;

    private void Awake()
    {
        dissolve = gameObject.GetComponent<Dissolve>();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" || col.tag == "Dashing")
        {
            if (!col.isTrigger && !isDissolved)
            {
                Dissolve();
                DissolveDoor();
                Sound.Post(gameObject);
                isDissolved = true;
            
            }
        }
    }

    void Dissolve()
    {
        dissolve.DissolveMe(dissolveTime);
    }

    void DissolveDoor()
    {
        door.Unlock();
    }
}
