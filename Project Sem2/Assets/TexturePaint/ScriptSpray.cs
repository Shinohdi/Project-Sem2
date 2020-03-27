using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSpray : MonoBehaviour
{
    [SerializeField] private Camera vision;
    [SerializeField] private Transform player;

    [SerializeField] private Texture2D splash;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(player.position, vision.transform.forward, out hit, Mathf.Infinity))
            {
                Debug.Log(hit.collider.name);
                MyShaderBehavior script = hit.collider.gameObject.GetComponent<MyShaderBehavior>();
                if (null != script)
                    script.PaintOn(hit.textureCoord, splash);
            }
        }
    }
}
