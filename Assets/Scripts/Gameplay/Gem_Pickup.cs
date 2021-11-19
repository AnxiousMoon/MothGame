using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem_Pickup : MonoBehaviour
{
    public GameObject Gem;

    public Gem_manager GemManager;


    // Start is called before the first frame update
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" || col.tag == "Dashing")
        {
            if(!col.isTrigger)
            {
                Gem.SetActive(false);
                GemManager.Count += 1;
            }
        }
    }
}
