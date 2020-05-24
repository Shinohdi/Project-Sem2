using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicFirstZone : MonoBehaviour
{

    [SerializeField] private Transform player;

    [FMODUnity.EventRef]
    public string EventMusique = "";
    FMOD.Studio.EventInstance Musique;

    float volume;

    // Start is called before the first frame update
    void Start()
    {
        Musique = FMODUnity.RuntimeManager.CreateInstance(EventMusique);
        Musique.start();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = transform.position - player.position;

        if (volume <= 90)
        {
            volume = -(distance.magnitude - 100);
            Musique.setParameterByName("VolumeDebut", volume);
        }
        else
        {
            volume = 100;
            Musique.setParameterByName("VolumeDebut", volume);
        }
        //Debug.Log(volume);

        // Pour test le pourcentage de panneaux terminés

        if(Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("2eme partie en cours...");
            Musique.setParameterByName("Zone1Fill", 26);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("3eme partie en cours...");
            Musique.setParameterByName("Zone1Fill", 52);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("4eme partie en cours...");
            Musique.setParameterByName("Zone1Fill", 76);
        }
    }
}
