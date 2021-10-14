using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashFX : MonoBehaviour
{
    [SerializeField] int numberOfTrailMeshes = 3;
    [SerializeField] GameObject playerDashGameObject;
    
    [SerializeField]
    GameObject startDashObj;
    DashGhost startDashDashGhost;

    [Header("Debugging")]
    [SerializeField] bool showDebugGizmos = true;

    GameObject playerObj;

    GameObject[] trailMeshGameObjects;
    DashGhost[] dashGhosts;

    Vector3 trailStartPos, trailEndPos;
    bool dashTrailIsInitialised = false;

    private void Start()
    {
        playerObj = transform.parent.gameObject;

        InitialisePlayerStartMesh();
        InitialiseTrailMeshes();
    }

        // This function is called by the player dash script
        // It projects an array of meshes behind the player 
    public void Activate(Vector3 startPos, int nTrailInstances)
    {
        if (dashTrailIsInitialised)
        {
            trailStartPos = startPos;
            trailEndPos = transform.position;
            float distance = Vector3.Distance(startPos, trailEndPos);
            for (int i = 0; i < numberOfTrailMeshes; i++)
            {
                trailMeshGameObjects[i].SetActive(true);
                trailMeshGameObjects[i].transform.position = (trailStartPos - trailEndPos) * (1f / (numberOfTrailMeshes + 1f)) * (i + 1);
                dashGhosts[i].FadeIn();

            }

            startDashObj.transform.position = startPos;
            startDashObj.SetActive(true);
            startDashDashGhost.FadeIn();

        }
    }


        // Creates the array of dash meshes with the player prefab
        // The dash meshes are only inititalised once but are activated and deactivated to improve performance
    private void InitialiseTrailMeshes()
    {
        trailMeshGameObjects = new GameObject[numberOfTrailMeshes];
        dashGhosts = new DashGhost[trailMeshGameObjects.Length];
        for(int i = 0; i < numberOfTrailMeshes; i++)
        {
            trailMeshGameObjects[i] = Instantiate(playerDashGameObject);
            trailMeshGameObjects[i].transform.parent = transform;
            dashGhosts[i] = trailMeshGameObjects[i].GetComponent<DashGhost>();
            trailMeshGameObjects[i].SetActive(false);
            
            dashTrailIsInitialised = true;
        }
    }

    private void InitialisePlayerStartMesh()
    {
        startDashObj = Instantiate(startDashObj);
        startDashObj.transform.parent = transform;


        startDashDashGhost = startDashObj.GetComponent<DashGhost>();

        startDashObj.SetActive(false);
    }

        // Tempoary activation with space key
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Activate(new Vector3(-10, 0, -15), numberOfTrailMeshes);
        }
    }

        // Gizmo is drawn in scene view to check the start position of the player
    private void OnDrawGizmos()
    {
        if (showDebugGizmos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(trailStartPos, 0.1f);
        }
    }
}
