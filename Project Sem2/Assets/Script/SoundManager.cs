using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public void PasSurSurfaceSimple(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, transform.position);
    }
}
