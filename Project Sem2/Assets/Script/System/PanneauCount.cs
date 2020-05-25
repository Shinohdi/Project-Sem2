using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanneauCount : MonoBehaviour
{

    [SerializeField] private ParticleDecalPool PDP;

    private int panneauInLevel;

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
            //2ere phase
            Debug.Log(PDP.PanneauClear);
        }

        if (PDP.PanneauClear >= panneauInLevel * 0.5f)
        {
            //3eme phase
        }

        if (PDP.PanneauClear >= panneauInLevel * 0.75f)
        {
            //4eme phase
        }
    }
}
