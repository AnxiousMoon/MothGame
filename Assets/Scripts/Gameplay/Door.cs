using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Dissolve door, padlock;

    [SerializeField] float initialDelay = 0.5f, initialDoorDelay = 0.5f, padlockDuration = 0.5f, doorDuration = 0.5f;
    public void Unlock()
    {
        StartCoroutine(UnlockSequence());
    }

    IEnumerator UnlockSequence()
    {
        yield return new WaitForSeconds(initialDelay);
        padlock.DissolveMe(padlockDuration);
        yield return new WaitForSeconds(initialDoorDelay);
        door.DissolveMe(doorDuration);
    }
}
