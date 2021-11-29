using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] Vector2 maxMinMoveSpeed = new Vector2(1, 5);
    float moveSpeed;
    private void Start()
    {
        moveSpeed = Random.Range(maxMinMoveSpeed.x, maxMinMoveSpeed.y);
        
    }

    private void Update()
    {
        transform.localPosition += moveSpeed * -transform.parent.forward * Time.deltaTime;
    }
}
