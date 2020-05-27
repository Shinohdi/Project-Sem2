using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanneauCount : MonoBehaviour
{

    [SerializeField] private ParticleDecalPool PDP;

    private int panneauInLevel;

    public musicFirstZone Musique;

    [SerializeField] private Slider slide;

    // Start is called before the first frame update
    void Start()
    {
        panneauInLevel = PDP.panneau.Count;
        slide.maxValue = panneauInLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if(PDP.PanneauClear >= panneauInLevel * 0.25f)
        {
            Musique.MusiqueFirst();
            slide.value = panneauInLevel * 0.25f;
        }

        if (PDP.PanneauClear >= panneauInLevel * 0.5f)
        {
            Musique.MusiqueSecond();
            slide.value = panneauInLevel * 0.5f;

        }

        if (PDP.PanneauClear >= panneauInLevel * 0.75f)
        {
            Musique.MusiqueThird();
            slide.value = panneauInLevel * 0.75f;

        }
    }
}
