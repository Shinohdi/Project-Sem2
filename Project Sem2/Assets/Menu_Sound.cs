using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Menu_Sound : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string EventAmbiance;
    FMOD.Studio.EventInstance Ambiance;

    private bool isPlaying;

    [FMODUnity.EventRef]
    public string EventOnClick;

    // Start is called before the first frame update
    void Start()
    {
        Ambiance = FMODUnity.RuntimeManager.CreateInstance(EventAmbiance);
        Ambiance.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            FMODUnity.RuntimeManager.PlayOneShot(EventOnClick, transform.position);
            Ambiance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            isPlaying = false;
        }
    }

    public void OnClickPlay()
    {
        isPlaying = true;
    }
}
