using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointPigeon : MonoBehaviour
{
    public GameObject prefabPigeon;
    public GameObject player;

    public int waitTime;

    [FMODUnity.EventRef]
    public string Event;

    void Start()
    {
        Instantiate(prefabPigeon, gameObject.transform);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            FMODUnity.RuntimeManager.PlayOneShot(Event, transform.position);
            // prefabPigeon.transform.position = new Vector3(prefabPigeon.transform.position.x, prefabPigeon.transform.position.y - 50, prefabPigeon.transform.position.z);


            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        Destroy(prefabPigeon);

        yield return new WaitForSeconds(waitTime);

        // prefabPigeon.transform.position = gameObject.transform.position;

        Instantiate(prefabPigeon, gameObject.transform);
    }
}
