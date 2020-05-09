using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonAmbianceSelonHauteur : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string Event;
    FMOD.Studio.EventInstance Audio;

    public float Hauteur;

    void Awake()
    {
        Audio = FMODUnity.RuntimeManager.CreateInstance(Event);
    }

    void Start()
    {
        Audio.start();
    }

    void Update()
    {
        Hauteur = gameObject.transform.position.y;

        Audio.setParameterByName("VentSelonHauteur", Hauteur);
    }
}
