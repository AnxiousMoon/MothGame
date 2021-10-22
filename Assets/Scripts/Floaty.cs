using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floaty : MonoBehaviour {

	private Vector3 newPosition;
	private Rigidbody rdb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		newPosition = transform.position;
    	newPosition.y += Mathf.Sin(2.5f * Time.time) * 1f * Time.deltaTime;
   		transform.position = newPosition;

   		transform.Rotate (1.5f, 0f, 0f * Time.deltaTime);
	}	

}
