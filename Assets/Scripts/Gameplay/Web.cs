using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{
    public Move move;
    Collider m_collider;
    float enableCol;
    // Start is called before the first frame update
    void Start()
    {
        m_collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        enableCol += Time.deltaTime;
        if (enableCol >= 1)
        {
            m_collider.enabled = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Player")
        {
            move.moveSpeed = 0f;
        }
        if (col.collider.tag == "Dashing")
        {
            m_collider.enabled = !m_collider.enabled;
            enableCol = 0;
        }
    }

    void OnCollisionExit(Collision col)
    {
    }
}
