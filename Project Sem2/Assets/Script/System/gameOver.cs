using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{

    [HideInInspector] public bool gameOverBool;

    [SerializeField] private Animator anim;
    [SerializeField] private float timeChangeLost;
    [SerializeField] private float timeChangeWin;
    [SerializeField] private string LevelHere;

    public musicFirstZone MusiqueStop;

    [SerializeField] private GameObject victoire;

    private float chrono;

    [FMODUnity.EventRef]
    public string EventVictory;

    private bool isPlaying = false;
    public bool victoryBool;

    // Start is called before the first frame update
    void Start()
    {
        gameOverBool = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverBool)
        {
            chrono += Time.deltaTime;
            FadeToLevel(1);

            if(chrono >= timeChangeLost)
            {
                MusiqueStop.MusiqueStop();
                SceneManager.LoadScene(LevelHere);
                gameOverBool = false;
            }
        }
        if(victoryBool)
        {
            chrono += Time.deltaTime;
            victoryFade(1);

            if(chrono >= timeChangeWin - 5.5)
            {
                victoire.SetActive(true);
                anim.SetTrigger("Finish");

                if(chrono >= timeChangeWin)
                {
                    MusiqueStop.MusiqueStop();
                    SceneManager.LoadScene(0);
                    victoryBool = false;
                    isPlaying = false;
                }
            }

                
        }

        if (Input.GetKeyDown(KeyCode.AltGr))
        {
            victoryBool = true;
        }
    }

    public void FadeToLevel(int LevelIndex)
    {
        anim.SetTrigger("Fadeout");
    }

    public void victoryFade(int LevelIndex)
    {
        if (!isPlaying)
        {
            isPlaying = true;
            FMODUnity.RuntimeManager.PlayOneShot(EventVictory, transform.position);

        }
        anim.SetTrigger("Fadeout");
    }
}
