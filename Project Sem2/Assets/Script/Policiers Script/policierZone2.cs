using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class policierZone2 : MonoBehaviour
{
    [SerializeField] private gameOver gO;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Avatar"))
        {
            gO.gameOverBool = true;
        }
    }

}
