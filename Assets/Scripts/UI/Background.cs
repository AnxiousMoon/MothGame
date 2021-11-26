using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] GameObject mainCameraObj;
    Camera mainCamera;
    [SerializeField] float distanceFromCamera, parallaxAmount = 0.5f;
    [SerializeField] [Range(0f, 1f)] float cameraSmoothness = 0.5f;

    [SerializeField] GameObject tilePrefab;
    [SerializeField] int tilesX = 4, tilesY = 2;
    GameObject[] tiles;

    Vector3 targetLocation;
    Vector3 camPosLast, camPosNow, deltaCamPos;


    private void Awake()
    {
        mainCameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera = mainCameraObj.GetComponent<Camera>();
    }

    private void Start()
    {
        
        cameraSmoothness = 1f - cameraSmoothness;
        camPosLast = mainCameraObj.transform.position;
        Debug.Log(mainCameraObj.transform.forward);
        StartCoroutine(SetCameraInitialPosition());
        tiles = new GameObject[tilesX * tilesY];
        InstantiateTiles();
    }

    IEnumerator SetCameraInitialPosition()
    {
        yield return new WaitForSeconds(0.05f);
        transform.position = mainCameraObj.transform.position + mainCameraObj.transform.forward * mainCamera.farClipPlane * 0.6f;
    }

    private void Update()
    {
        camPosLast = camPosNow;
        camPosNow = mainCameraObj.transform.position;


        //get change in cameras position
        deltaCamPos = camPosNow - camPosLast;
        transform.position = Vector3.Lerp(transform.position, (transform.position + deltaCamPos * parallaxAmount), cameraSmoothness);
    }

    void InstantiateTiles()
    {
        float spacing = tilePrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size.x * transform.localScale.x * 2;
        for (int y = 0; y < tilesY; y++)
        {
            float yPos = (y - Mathf.FloorToInt(tilesY / 2)) * spacing;
            for (int x = 0; x < tilesX; x++)
            {
                tiles[x] = Instantiate(tilePrefab, transform);
                tiles[x].transform.localPosition = new Vector3((x - Mathf.FloorToInt(tilesX / 2)) * spacing,
                                                                0,yPos);
            }
        }
    }
}
