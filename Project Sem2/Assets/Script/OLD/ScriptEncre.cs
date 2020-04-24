using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEncre : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            if(gameObject.layer == 9)
            {
                Debug.Log("Rouge");
                if (other.gameObject.CompareTag("Player"))
                {
                    Debug.Log("ca marche");
                    //other.gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 50, ForceMode.Impulse);

                }
                
            }

            if (gameObject.layer == 10)
            {
                Debug.Log("Bleu");

            }
        }
    }
}
