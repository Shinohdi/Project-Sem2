using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotateCamera : MonoBehaviour {

	public float speed = 10;

	bool Lock = true;

	void FixedUpdate () {
		
		if(Lock == true)
		{
			Screen.lockCursor = true;
			transform.Rotate(-Input.GetAxis("Mouse Y") * speed, 0, 0);
			// Debug.Log(Input.GetAxis("Mouse Y")); => donner un maximum et un minmum pour que la camera ne fasse pas de 360 (A FAIRE PLUS TARD)
			if(Input.GetKey(KeyCode.Escape))
			{
				Lock = false;
			}
		}

		if(Lock == false)
		{
			Screen.lockCursor = false;
			if(Input.GetMouseButtonDown(0))
			{
				Lock = true;
			}
		}
	}
}