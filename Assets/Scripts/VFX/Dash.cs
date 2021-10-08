using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] int numberOfTrailMeshes = 3;
    [SerializeField] GameObject playerDashGameObject;

    GameObject PlayerObject;
    GameObject[] trailMeshGameObjects;
    DashGhost[] dashGhosts;
    Vector3 trailStartPos, trailEndPos;
    bool dashTrailIsInitialised = false;

    Animator animator;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();

    }

    private void Start()
    {
        PlayerObject = transform.parent.gameObject;
        InitialiseTrailMeshes();
        
    }
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
                Debug.Log((trailEndPos - startPos) + " " + (1 / (nTrailInstances + 1))+ " " + (i + 1));
                dashGhosts[i].FadeIn();

            }
        }
    }

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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Activate(new Vector3(-10, 0, -15), numberOfTrailMeshes);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(trailStartPos, 0.1f);
    }
}
