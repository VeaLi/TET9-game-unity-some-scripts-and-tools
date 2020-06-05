using UnityEngine;
using System.Collections;


//this helps
//https://forum.unity.com/threads/rotating-cube-script.170827/


//this class for smoothly rotatation of the objects. Mainly crated for collectables, and their icons

public class FruitRotation : MonoBehaviour {

	public Vector3 RotateAmount;  // degrees per second to rotate in each axis. Set in inspector.

	// Update is called once per frame
	void Update () {
		transform.Rotate(RotateAmount * Time.deltaTime);
	}
}