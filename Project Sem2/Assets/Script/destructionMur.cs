using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructionMur : MonoBehaviour
{
    //[SerializeField] private int pdvMur = 1;
    [SerializeField] private Rigidbody RB;

    void Start()
    {
        
    }


    void OnCollisionEnter (Collision col)
    {
        
        if (col.transform.CompareTag("Bille"))
        {
            Debug.Log("DETRUIT");
            Destroy(gameObject);
        }
    }
}
