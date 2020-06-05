using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this helps and goes after
//https://answers.unity.com/questions/22130/how-do-i-make-an-object-always-face-the-player.html


//this class is for lockig objects view on player
public class LookAtTarget : MonoBehaviour
{

	//public Transform target;
	public Camera targetCamera;

	// Update is called once per frame
	void Update()
	{
		transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.left,
		                 targetCamera.transform.rotation * Vector3.up);
	}
}
