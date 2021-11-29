using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    Vector3 direction;
    [SerializeField] Vector2 spawnRateMinMax = new Vector2(5, 10);
    [SerializeField] GameObject cloudPrefab;
    BoxCollider boxCollider;

    private void Awake()
    {
        direction = transform.right;
        StartCoroutine(SpawnCloud());
        boxCollider = gameObject.GetComponent<BoxCollider>();
    }

    IEnumerator SpawnCloud()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnRateMinMax.x, spawnRateMinMax.y));
            GameObject _cloud = Instantiate(cloudPrefab, transform);
            _cloud.transform.position = transform.position;
            _cloud.transform.position += Random.Range(boxCollider.size.x * -0.5f, boxCollider.size.x * 0.5f) * transform.right;
        }

    }

}
