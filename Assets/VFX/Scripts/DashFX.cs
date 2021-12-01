using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashFX : MonoBehaviour
{
    [SerializeField] GameObject standingMoth;
    [SerializeField] GameObject dashingMoth;
    [SerializeField] float ghostEmissionDistance = 1f;
    [SerializeField] float dashDuration = 1f;

    Vector3 startPos;
    float currentDist;

    bool isDashing = false;

   

    public void Activate()
    {
        isDashing = true;
        startPos = transform.position;
        DropGhost(standingMoth);
        StartCoroutine(cooldown());
    }


    private void Update()
    {
        if (isDashing)
        {
            currentDist = Vector3.Distance(startPos, transform.position);
            
            if(currentDist > ghostEmissionDistance)
            {
                DropGhost(dashingMoth);
                currentDist = 0f;
                startPos = transform.position;
            }
        }
    }
    void DropGhost(GameObject _ghost)
    {
        _ghost = Instantiate(_ghost);
        _ghost.transform.position = transform.position;
        _ghost.transform.forward = transform.forward;

    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }
}
