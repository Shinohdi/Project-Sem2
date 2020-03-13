using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string Event;
    FMOD.Studio.EventInstance Audio;

    void Awake()
    {
        Audio = FMODUnity.RuntimeManager.CreateInstance(Event);
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "TexturePeinture")
        {
            Debug.Log("Paint");
            Audio.setParameterByName("PasAvecPeinture", 1);

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TexturePeinture")
        {
            Debug.Log("No Paint");
            Audio.setParameterByName("PasAvecPeinture", 0);
        }
    }

    public void PasSurSurfaceSimple(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, transform.position);
    }
}
