using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class tagScore : MonoBehaviour
{

    [HideInInspector] public float score;
    [SerializeField] private int scoreMax;

    public float multiplicateur;

    private bool isEnter;
    public bool isScoring;
    public bool Completed;

    [SerializeField] private GameObject preScoreBar;
    [SerializeField] private Canvas canvas;

    private Slider bar;

   
    private GameObject scoreBar;

    [FMODUnity.EventRef]
    public string EventRecompense = "";

    private void Start()
    {
        isEnter = false;
    }

    private void Update()
    {
        if (isScoring && !Completed)
        {
            bar.value = score;

            if(bar.value >= bar.maxValue)
            {
                FMODUnity.RuntimeManager.PlayOneShot(EventRecompense, transform.position);
                Completed = true;
                Destroy(scoreBar);
            } 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Avatar") && !Completed)
        {
            if (!isEnter)
            {
                isEnter = true;
                scoreBar = Instantiate(preScoreBar, canvas.transform);
                scoreBar.name = gameObject.name + " ScoreBar";
                bar = scoreBar.GetComponent<Slider>();
                bar.maxValue = scoreMax;
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
        if (other.CompareTag("Avatar") && !Completed)
        {
            scoreBar.SetActive(false);
            isScoring = false;
        }
    }
}
