using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Switch_Cave : MonoBehaviour
{
    public AK.Wwise.Switch Cave;

    public GameObject Player;
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player") ;
        {
            Cave.SetValue(Player);
        }

    }
}
