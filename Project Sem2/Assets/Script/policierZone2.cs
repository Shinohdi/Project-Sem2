using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class policierZone2 : MonoBehaviour
{
    void OnTriggerStay(Collider collision)
    {
        if (collision.transform.CompareTag("Avatar"))
        {
            Debug.Log("2");
            Application.LoadLevel(1);

        }
    }

}
