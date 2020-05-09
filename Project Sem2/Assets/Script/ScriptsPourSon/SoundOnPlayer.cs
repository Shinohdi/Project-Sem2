using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMODUnity
{
    public class SoundOnPlayer : MonoBehaviour
    {
        [EventRef]
        public string EventTir = "";
        [EventRef]
        public string EventRelacher = "";


        FMOD.Studio.EventInstance tir;

        public bool AllowFadeout = true;

        // Start is called before the first frame update
        void Start()
        {
            tir = FMODUnity.RuntimeManager.CreateInstance(EventTir);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                tir.start();

            }

            if (Input.GetButtonUp("Fire1"))
            {
                FMODUnity.RuntimeManager.PlayOneShot(EventRelacher, transform.position);
                tir.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }

           
        }
    }
}


