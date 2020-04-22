using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class policierZone2 : MonoBehaviour
{

    public Animator animator;
    [SerializeField] private float compT = 1f;
    [SerializeField] private float count = 0f;
    private bool gameOver = false;


    void Update()
    {
        if (gameOver == true)
        {
            count += Time.deltaTime;
            //Debug.Log("2");
            FadeToLevel(1);

            if (count > compT)
            {
                Application.LoadLevel(1);
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
