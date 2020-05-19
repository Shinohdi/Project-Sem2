using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tagScore : MonoBehaviour
{

    public float score;
    [SerializeField] private int scoreMax;

    private bool isEnter;
    public bool isScoring;

    [SerializeField] private GameObject preScoreBar;
    [SerializeField] private Canvas canvas;

   
    private GameObject scoreBar;

    private void Start()
    {
        isEnter = false;
    }

    private void Update()
    {
        if (isScoring)
        {
            scoreBar.GetComponent<Slider>().value = score;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Avatar"))
        {
            if (!isEnter)
            {
                isEnter = true;
                scoreBar = Instantiate(preScoreBar, canvas.transform);
                scoreBar.name = gameObject.name + " ScoreBar";
                scoreBar.GetComponent<Slider>().maxValue = scoreMax;
                isScoring = true;
                

            }
            else
            {
                scoreBar.SetActive(true);
                isScoring = true;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Avatar"))
        {
            scoreBar.SetActive(false);
            isScoring = false;
        }
    }
}
