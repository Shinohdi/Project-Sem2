using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deplacementAvatar : MonoBehaviour
{

    [SerializeField] private float vitesse;

    private float vitesseIni;

    [SerializeField] private float vitesseMax;


    // Start is called before the first frame update
    void Start()
    {
        vitesseIni = vitesse;
    }

    // Update is called once per frame
    void Update()
    {
        /*float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v);

        move = Vector3.ClampMagnitude(move, 1f);

        Vector3 position = transform.position;

        position += move * Time.deltaTime * vitesse;

        transform.position = position;*/

        if (Input.GetKey(KeyCode.Z))
        {
            transform.Translate(0, 0, vitesse * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(vitesse != vitesseMax)
            {
                vitesse = vitesseMax;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (vitesse != vitesseIni)
            {
                vitesse = vitesseIni;
            }
        }

    }
}
