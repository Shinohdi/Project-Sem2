using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class policierZone1 : MonoBehaviour
{
    [SerializeField] private Image customImage;


    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Avatar"))
        {
            Debug.Log("+1");
            customImage.enabled = true;
        }
    }

    void OnTriggerExit(Collider collid)
    {
        if (collid.transform.CompareTag("Avatar"))
        {
            Debug.Log("-1");
            customImage.enabled = false;
        }
    }

}
