using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSonIsTriggered : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Avatar"))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 20, gameObject.transform.position.z);
        }
    }
}
