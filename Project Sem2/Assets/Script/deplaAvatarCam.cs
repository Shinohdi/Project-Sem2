using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deplaAvatarCam : MonoBehaviour
{

    [SerializeField] private int vitesse;

    private int vitesseMin;

    [SerializeField] private int vitesseMax;


    // Start is called before the first frame update
    void Start()
    {
        vitesseMin = vitesse;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            transform.Translate(0, 0, vitesse * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            vitesse = vitesseMax;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            vitesse = vitesseMin;
        }
    }
}
