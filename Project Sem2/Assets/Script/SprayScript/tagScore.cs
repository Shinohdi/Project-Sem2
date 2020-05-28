using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class tagScore : MonoBehaviour
{

    [HideInInspector] public float score;
    [SerializeField] private int scoreMax;

    [SerializeField] private Light LumGauche;
    [SerializeField] private Light LumMid;
    [SerializeField] private Light LumDroite;


    public float multiplicateur;

    private bool isEnter;
    public bool isScoring;
    public bool Completed;

    [SerializeField] private GameObject preScoreBar;
    [SerializeField] private Canvas canvas;

    private Slider bar;

    [SerializeField] private float timeChangeColor;
    private float chrono;

   
    private GameObject scoreBar;

    [FMODUnity.EventRef]
    public string EventRecompense = "";

    public enum colorTag
    {
        None,
        Red,
        Blue,
        Green
    }

    public colorTag color = colorTag.None;

    void switchColor(colorTag newColor)
    {
        OnExitColor();
        color = newColor;
        OnEnterColor();

    }

    void OnEnterColor()
    {
        switch (color)
        {
            case colorTag.None:
                LumDroite.color = Color.red;
                LumMid.color = Color.blue;
                LumGauche.color = Color.green;
                break;
            case colorTag.Red:
                LumDroite.color = Color.red;
                LumGauche.color = Color.red;
                LumMid.color = Color.red;
                break;
            case colorTag.Blue:
                LumDroite.color = Color.blue;
                LumGauche.color = Color.blue;
                LumMid.color = Color.blue;
                break;
            case colorTag.Green:
                LumDroite.color = Color.green;
                LumGauche.color = Color.green;
                LumMid.color = Color.green;
                break;
        }
    }

    void OnUpdateColor()
    {
        switch (color)
        {
            case colorTag.None:
                chrono += Time.deltaTime;

                if (chrono >= timeChangeColor)
                {
                    colorTag colorRandom = (colorTag)Random.Range(1, 4);
                    switchColor(colorRandom);
                    chrono = 0;
                }

                break;
            case colorTag.Red:
                chrono += Time.deltaTime;

                if (chrono >= timeChangeColor)
                {
                    switchColor(colorTag.None);
                    chrono = 0;
                }

                break;
            case colorTag.Blue:
                chrono += Time.deltaTime;

                if (chrono >= timeChangeColor)
                {
                    switchColor(colorTag.None);
                    chrono = 0;
                }

                break;
            case colorTag.Green:
                chrono += Time.deltaTime;

                if (chrono >= timeChangeColor)
                {
                    switchColor(colorTag.None);
                    chrono = 0;
                }

                break;
        }
    }

    void OnExitColor()
    {
        switch (color)
        {
            case colorTag.None:
                break;
            case colorTag.Red:
                break;
            case colorTag.Blue:
                break;
            case colorTag.Green:
                break;
        }
    }

    private void Start()
    {
        isEnter = false;
    }

    private void Update()
    {
        if (isScoring && !Completed)
        {
            bar.value = score;

            OnUpdateColor();

            if(bar.value >= bar.maxValue)
            {
                FMODUnity.RuntimeManager.PlayOneShot(EventRecompense, transform.position);
                Completed = true;
                LumDroite.color = Color.white;
                LumMid.color = Color.white;
                LumGauche.color = Color.white;
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
