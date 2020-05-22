using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicFirstZone : MonoBehaviour
{

    [SerializeField] private Transform player;

    public float threshold;

    private bool isEnter;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = transform.position - player.position;

        if (!isEnter)
        {
            if (distance.magnitude > threshold)
            {
                Debug.Log("Le son est entendu");
            }
            else
            {
                Debug.Log("le son est a son volume max");
                isEnter = true;
            }
        }
    }
}
