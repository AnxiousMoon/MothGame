using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floaty : MonoBehaviour {

	private Vector3 newPosition;
	private Rigidbody rdb;
	[SerializeField] float rotationSpeedMultiplier = 1f;
	public int num;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		switch (num)
		{
			case 1:
			newPosition = transform.position;
    		newPosition.y += Mathf.Sin(2.5f * Time.time) * 1f * Time.deltaTime;
   			transform.position = newPosition;

   			transform.Rotate (.3f, 0f, 0f * Time.deltaTime);
   			break;

   			case 2:
   			newPosition = transform.position;
    		newPosition.y += Mathf.Sin(2.5f * Time.time) * 1f * Time.deltaTime;
   			transform.position = newPosition;

   			transform.Rotate (0f, 0f, .2f * Time.deltaTime* rotationSpeedMultiplier);
   			break;


			default:
   			break;
		}
		
	}	

}
