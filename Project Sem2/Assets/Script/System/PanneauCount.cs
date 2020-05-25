using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanneauCount : MonoBehaviour
{

    [SerializeField] private ParticleDecalPool PDP;

    private int panneauInLevel;

    public musicFirstZone Musique;

    // Start is called before the first frame update
    void Start()
    {
        panneauInLevel = PDP.panneau.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(PDP.PanneauClear >= panneauInLevel * 0.25f)
        {
            Musique.MusiqueFirst();
            Debug.Log(PDP.PanneauClear);
        }

        if (PDP.PanneauClear >= panneauInLevel * 0.5f)
        {
            Musique.MusiqueSecond();
        }

        if (PDP.PanneauClear >= panneauInLevel * 0.75f)
        {
            Musique.MusiqueThird();
        }
    }
}
