using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwitch : MonoBehaviour
{
    public AK.Wwise.Switch Cave;
    public AK.Wwise.Switch Platform;

    public GameObject Player;
    private void OnTriggerEnter(Collider other)
    {
        Platform.SetValue(Player);
    }

    private void OnTriggerExit(Collider other)
    {
        Cave.SetValue(Player);
    }
}
