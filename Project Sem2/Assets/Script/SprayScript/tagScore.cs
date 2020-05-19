using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tagScore : MonoBehaviour
{

    public float score;

    private bool isEnter;

    [SerializeField] private GameObject preScoreBar;
    [SerializeField] private Canvas canvas;

    private GameObject scoreBar;

    private void Start()
    {
        isEnter = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Avatar") && !isEnter)
        {
            isEnter = true;
            Debug.Log("oui");
            scoreBar = Instantiate(preScoreBar, transform.position, transform.rotation, canvas.transform);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Avatar"))
        {
            
        }
    }
}
