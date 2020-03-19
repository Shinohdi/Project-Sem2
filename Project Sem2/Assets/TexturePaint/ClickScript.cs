using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickScript : MonoBehaviour {
    public Camera vision;
    public Transform player;
    public Texture2D splashTexture;
	
	void Update ()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = vision.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(player.position, vision.transform.forward, out hit, Mathf.Infinity))
            {
                Debug.Log(hit.collider.name);
                MyShaderBehavior script = hit.collider.gameObject.GetComponent<MyShaderBehavior>();
                if (null != script)
                    script.PaintOn(hit.textureCoord, splashTexture);
            }
        }
	}
}
