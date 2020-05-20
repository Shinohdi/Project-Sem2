using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class policierZone2 : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private float compT = 1f;
    [SerializeField] private float count = 0f;
    public bool gameOver = false;


    void Update()
    {           
            if (gameOver == true)
            {
                count += Time.deltaTime;
                //Debug.Log("2");
                FadeToLevel(1);

                if (count > compT)
                {
                    SceneManager.LoadScene("TestLD");
                }
            }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Avatar"))
        {
            gameOver = true;
        }
    }


    public void FadeToLevel(int LevelIndex)
    {
        animator.SetTrigger("Fadeout");
    }

}
