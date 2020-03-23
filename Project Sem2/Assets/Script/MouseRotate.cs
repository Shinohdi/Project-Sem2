using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotate : MonoBehaviour {

    [SerializeField] private float speed;

	bool Lock = true;

	void FixedUpdate () {
		// transform.Rotate(0, Input.GetAxis("Mouse X"), 0 * Time.deltaTime * speed);

		if(Lock == true)
		{
			Screen.lockCursor = true;
			transform.Rotate(0, Input.GetAxis("Mouse X") * speed, 0);
			// transform.Rotate(-Input.GetAxis("Mouse Y"), 0, 0 * Time.deltaTime * speed);
			// // Debug.Log(Input.GetAxis("Mouse Y")); => donner un maximum et un minmum pour que la camera ne fasse pas de 360 (A FAIRE PLUS TARD)
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
