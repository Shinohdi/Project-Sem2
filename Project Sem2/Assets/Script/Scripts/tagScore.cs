using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tagScore : MonoBehaviour
{
    public bool isScoring;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("score"))
        {
            isScoring = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("score"))
        {
            isScoring = false;
        }
    }
}
