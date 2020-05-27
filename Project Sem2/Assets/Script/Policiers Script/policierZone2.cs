using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class policierZone2 : MonoBehaviour
{
    [SerializeField] private gameOver gO;

    [EventRef]
    public string EventSpotted = "";

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Avatar"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(EventSpotted, transform.position);
            gO.gameOverBool = true;
        }
    }

}
