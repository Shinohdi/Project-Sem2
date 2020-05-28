using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeInLevel : MonoBehaviour
{

    [SerializeField] private float minutes;
    [SerializeField] private float secondes;

    [SerializeField] private Text time;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(secondes <= 0)
        {
            minutes--;
            secondes = 60f;
        }


        secondes -= Time.deltaTime;

        time.text = string.Format("{0} m {1} s", minutes, (int)secondes);

    
        if(minutes <= 0)
        {
            GetComponent<gameOver>().gameOverBool = true;
            
        }
    }
}
