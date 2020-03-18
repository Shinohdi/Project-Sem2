using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDepla : MonoBehaviour
{

    [SerializeField] private int vitesse;
    [SerializeField] private float JForce;
    [SerializeField] private float gravité = 9.81f;
    [SerializeField] private int vitesseMax;


    private int vitesseMin;

    private Animator anim;

    private CharacterController CC;
    private Vector3 move = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

        vitesseMin = vitesse;

        CC = GetComponent<CharacterController>();

        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        if (CC.isGrounded)
        {
            move = new Vector3(0, 0, v * vitesse);
            move = transform.TransformDirection(move);

            if (Input.GetButton("Jump"))
            {
                move.y = JForce;
            }
          
        }

        if(v > 0)
        {
            anim.SetBool("isWalkingForward", true);
            anim.SetBool("isIdle", false);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                vitesse = vitesseMax;
                anim.SetBool("isRunning", true);
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                vitesse = vitesseMin;
                anim.SetBool("isRunning", false);
            }
        }
        else if (v < 0)
        {
            anim.SetBool("isWalkingBackward", true);
            anim.SetBool("isIdle", false);
        }
        else if(v == 0)
        {
            anim.SetBool("isWalkingForward", false);
            anim.SetBool("isWalkingBackward", false);
            anim.SetBool("isIdle", true);
            anim.SetBool("isRunning", false);
        }

        move.y -= gravité * Time.deltaTime;

        CC.Move(move * Time.deltaTime);

    }
}
