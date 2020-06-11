using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainmenu : MonoBehaviour
{

    private bool play = false;

    public float chronoMax;
    private float chrono;




    public void Playgame ()
    {
        play = true;
    }

    public void Quitgame ()
    {
        Debug.Log("Quitter;");
        Application.Quit();

    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if(play == true)
        {
            chrono += Time.deltaTime;

            if(chrono >= chronoMax)
            {
                SceneManager.LoadScene("TestLD");
                play = false;
            }
        }
    }




}
