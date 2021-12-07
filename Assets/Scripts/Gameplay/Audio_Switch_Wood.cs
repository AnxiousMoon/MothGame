using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Switch_Wood : MonoBehaviour
{
    public AK.Wwise.Switch Platform;
    public AK.Wwise.Switch Cave;

    public GameObject Player;

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player" || col.tag == "Dashing")
        {
            Platform.SetValue(Player);
        }

    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player" || col.tag == "Dashing")
        {
            Cave.SetValue(Player);
        }

    }
}
