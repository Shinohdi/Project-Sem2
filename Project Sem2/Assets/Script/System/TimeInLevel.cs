using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeInLevel : MonoBehaviour
{

    [SerializeField] private int TimeMax;
    private float chrono;


    // Start is called before the first frame update
    void Start()
    {
        chrono = TimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        chrono -= Time.deltaTime;

        if(chrono <= 0)
        {
            GetComponent<gameOver>().gameOverBool = true;
            chrono = TimeMax;
        }
    }
}
