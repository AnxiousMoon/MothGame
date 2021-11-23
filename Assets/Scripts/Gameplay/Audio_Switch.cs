using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Switch : MonoBehaviour
{
    public AK.Wwise.Switch Cave;
    public AK.Wwise.Switch Platform;

    public GameObject Player;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player");
        {
            Platform.SetValue(Player);
        }

    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player") ;
        {
            Cave.SetValue(Player);
        }
    }
}
