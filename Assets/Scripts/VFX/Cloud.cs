using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] Vector2 maxMinMoveSpeed = new Vector2(1, 5);
    float moveSpeed;
    BoxCollider safeArea;
    private void Start()
    {
        moveSpeed = Random.Range(maxMinMoveSpeed.x, maxMinMoveSpeed.y);
        safeArea = transform.parent.GetComponent<BoxCollider>();

        
    }
    private void OnTriggerExit(Collider other)
    {
         if (other.name == "CloudSpawner")
        { 
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.localPosition += moveSpeed * -transform.forward * Time.deltaTime;
    }
}
