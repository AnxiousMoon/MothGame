using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem_Pickup : MonoBehaviour
{
    public GameObject Gem;

    public Gem_manager GemManager;

    Dissolve dissolve;

    private void Awake()
    {
        dissolve = gameObject.GetComponent<Dissolve>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" || col.tag == "Dashing")
        {
            if(!col.isTrigger)
            {
                dissolve.DissolveMe(1f, true);
                GemManager.Count += 1;
            }
        }
    }
}
