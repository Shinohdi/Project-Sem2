using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSonAnimation : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string EventPas = "";

    public void JoueSonPas()
    {
        FMODUnity.RuntimeManager.PlayOneShot(EventPas, transform.position);
    }
}
