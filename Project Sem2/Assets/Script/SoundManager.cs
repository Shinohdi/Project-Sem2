using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    /*[FMODUnity.EventRef]
    public string Event;
    FMOD.Studio.EventInstance Audio;*/

    public bool surPeinture;

    /*void Awake()
    {
        Audio = FMODUnity.RuntimeManager.CreateInstance(Event);
    }*/

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "TexturePeinture")
        {
            Debug.Log("Paint");
            /*Audio.setParameterByName("PasAvecPeinture", 1);*/

            surPeinture = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TexturePeinture")
        {
            Debug.Log("No Paint");
            /*Audio.setParameterByName("PasAvecPeinture", 0);*/

            surPeinture = false;
        }
    }

    public void PasSurSurfaceSimple()
    {
        if (surPeinture == false)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Avatar/Deplacements/SurfaceSimple/Pas", transform.position);
        }
        if(surPeinture == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Avatar/Deplacements/SurfaceAvecPeinture/PasSurPeinture", transform.position);
        }
        
    }
}
