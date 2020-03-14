using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorBoule : MonoBehaviour
{

    [SerializeField] private float forceLancée;


    [SerializeField] private GameObject PotRouge;
    [SerializeField] private GameObject PotBleu;
    [SerializeField] private GameObject bouleParent;

    [SerializeField] private Transform main;

    [SerializeField] private GameObject camPlayer;


    [SerializeField] private GameObject Boule;

    private MeshRenderer MSBoule;


    private bool EnJoue;

    private Rigidbody RB;

    private enum stateBoule
    {
        SansPeinture,
        PeintureRouge,
        PeintureBleu
    }

    private stateBoule state = stateBoule.SansPeinture;


    // Start is called before the first frame update
    void Start()
    {
        EnJoue = true;
        MSBoule = Boule.GetComponent<MeshRenderer>();
        RB = Boule.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnJoue)
        {
            var disPotRouge = Vector3.Distance(transform.position, PotRouge.transform.position);
            var disPotBleu = Vector3.Distance(transform.position, PotBleu.transform.position);

            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log(camPlayer.transform.localEulerAngles.x);
                if (state == stateBoule.PeintureBleu)
                {
                    RB.isKinematic = false;
                    Boule.transform.parent = bouleParent.transform;
                    //Debug.Log(RB);
                    RB.AddForce(camPlayer.transform.forward * forceLancée, ForceMode.Impulse);
                    EnJoue = false;
                }
                else if(state == stateBoule.PeintureRouge)
                {
                    RB.isKinematic = false;
                    Boule.transform.parent = bouleParent.transform;
                    //Debug.Log(RB);
                    RB.AddForce(camPlayer.transform.forward * forceLancée, ForceMode.Impulse);
                    EnJoue = false;
                }

            }


            if (Input.GetKeyDown(KeyCode.E))
            {
                if (disPotBleu < 1.4f)
                {
                    if (state != stateBoule.PeintureBleu)
                    {

                        MSBoule.material.color = PotBleu.GetComponent<MeshRenderer>().material.color;
                        state = stateBoule.PeintureBleu;
                    }
                }

                if (disPotRouge < 1.4f)
                {
                    if (state != stateBoule.PeintureRouge)
                    {
                        MSBoule.material.color = PotRouge.GetComponent<MeshRenderer>().material.color;
                        state = stateBoule.PeintureRouge;

                    }
                }
            }
        }
        else
        {
            var disBoule = Vector3.Distance(transform.position, Boule.transform.position);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if(disBoule < 1.7f)
                {
                    Boule.transform.position = main.position;
                    Boule.transform.parent = transform;
                    RB.isKinematic = true;
                    EnJoue = true;
                }
            }
        }

    }
}
