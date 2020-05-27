using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splatOnFlic : MonoBehaviour
{

   
    RaycastHit hit;

    private patrol policier;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Debug.DrawRay(transform.position, transform.forward);

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.collider.CompareTag("police"))
                {
                    policier = hit.collider.gameObject.GetComponent<patrol>();
                    if(policier.state == patrol.State.Chase)
                    {
                        policier.SwitchState(patrol.State.Blind);

                    }
                }
            }
        }
    }

   
}
