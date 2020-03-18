using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDepla : MonoBehaviour
{

    [SerializeField] private float vitesse;
    [SerializeField] private float JForce;
    [SerializeField] private float gravité = 9.81f;



    private CharacterController CC;
    private Vector3 move = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        CC = GetComponent<CharacterController>();
       
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        Debug.Log(CC.isGrounded);
        if (CC.isGrounded)
        {
            move = new Vector3(0, 0, v * vitesse);
            move = transform.TransformDirection(move);
          
        }

        move.y -= gravité * Time.deltaTime;

        CC.Move(move * Time.deltaTime);

    }
}
