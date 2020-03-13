using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deplaAvatarCam : MonoBehaviour
{

    [SerializeField] private int vitesse;

    private int vitesseMin;

    [SerializeField] private int vitesseMax;

    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        vitesseMin = vitesse;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //isWalkingForward
        if (Input.GetKey(KeyCode.Z))
        {
            transform.Translate(0, 0, vitesse * Time.deltaTime);
            anim.SetBool("isWalkingForward", true);
            anim.SetBool("isIdle", false);
            Debug.Log("enMarche");
        }
        //isIdle
        else if(Input.GetKeyUp(KeyCode.Z))
        {
            anim.SetBool("isWalkingForward", false);
            anim.SetBool("isIdle", true);
        }

        //isWalkingBackward
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -vitesse * Time.deltaTime);
            anim.SetBool("isWalkingBackward", true);
            anim.SetBool("isIdle", false);
        }
        //isIdle
        else if(Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("isWalkingBackward", false);
            anim.SetBool("isIdle", true);
        }

        //isRunning
        if (Input.GetKey(KeyCode.LeftShift))
        {
            vitesse = vitesseMax;
            anim.SetBool("isRunning", true);
        }

        //endRunning
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            vitesse = vitesseMin;
            anim.SetBool("isRunning", false);
        }
    }
}
