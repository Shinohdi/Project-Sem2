using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{

    [HideInInspector] public bool gameOverBool;

    [SerializeField] private Animator anim;
    [SerializeField] private float timeChange;
    [SerializeField] private string LevelHere;

    public musicFirstZone MusiqueStop;

    private float chrono;


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

            if(chrono >= timeChange)
            {
                MusiqueStop.MusiqueStop();
                SceneManager.LoadScene(LevelHere);
                gameOverBool = false;
            }
        }
    }

    public void FadeToLevel(int LevelIndex)
    {
        anim.SetTrigger("Fadeout");
    }
}
