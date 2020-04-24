using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class magnet : MonoBehaviour
{
    private Rigidbody RB;

    private RaycastHit hit;

    [SerializeField] private Camera vision;
    [SerializeField] private int power;

    [SerializeField] private Slider puissanceSlid;

    [SerializeField] private int puissanceMax;

    [SerializeField] private Transform parentBille;


    private List<Transform> activObjects;
    private List<Transform> gravObjects;

    private float puissanceStock = 0;

    private bool push;


    private Color colorIni;

    // Start is called before the first frame update
    void Awake()
    {
        activObjects = new List<Transform>();
        gravObjects = new List<Transform>();

        puissanceSlid.maxValue = puissanceMax;
   
    }

    

    // Update is called once per frame
    void Update()
    {
        Ray rayon = vision.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(rayon, out hit, Mathf.Infinity, LayerMask.GetMask("ObjectMagnet"))) //Layer spéciale pour ne pas activer tous les objets
            {
                if (!activObjects.Contains(hit.collider.transform))
                {
                    activObjects.Add(hit.collider.transform); //Quand on clique sur l'objet, on active celui-ci afin de l'attirer
                    Debug.Log("touché");
                    colorIni = hit.collider.gameObject.GetComponent<MeshRenderer>().material.color;
                    hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                }


            }
        }

        AttireObjets();

        if(gravObjects.Count > 0)
        {

            if (Input.GetKey(KeyCode.A) && puissanceStock < puissanceMax + 0.5) //Charge la puissance de l'impulsion
            {
                puissanceStock += 10 * Time.deltaTime;
                puissanceSlid.value = puissanceStock;

            }

            if(puissanceStock >= puissanceMax) //Si la puissance atteint un seuil maximal, celui fait automatiquement l'impulsion (WIP)
            {
                for (int i = 0; i < gravObjects.Count; ++i)
                {
                    RB = gravObjects[i].GetComponent<Rigidbody>();

                    gravObjects[i].GetComponent<turnAround>().isNextTo = false;

                    RB.AddForce(gravObjects[i].forward * puissanceStock, ForceMode.Impulse);

                    gravObjects[i].parent = parentBille;

                    gravObjects[i].GetComponent<MeshRenderer>().material.color = colorIni;
                    
                }
                gravObjects.Clear();
                puissanceSlid.value = puissanceStock;
                puissanceStock = 0;
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                push = true;
            }

            if(push == true) //Si le joueur lache le bouton, l'impulsion se fait (WIP)
            {
                for (int i = 0; i < gravObjects.Count; ++i)
                {
                    RB = gravObjects[i].GetComponent<Rigidbody>();

                    gravObjects[i].GetComponent<turnAround>().isNextTo = false;

                    RB.AddForce(gravObjects[i].forward * puissanceStock, ForceMode.Impulse);

                    RB.constraints = RigidbodyConstraints.None;


                    gravObjects[i].parent = parentBille;

                    gravObjects[i].GetComponent<MeshRenderer>().material.color = colorIni;
                    
                }
                gravObjects.Clear();
                puissanceStock = 0;
                puissanceSlid.value = puissanceStock;
                push = false;
            }

        }

    }

    private void AttireObjets()
    {
        if (activObjects.Count > 0)
        {
            if (Input.GetMouseButton(1))
            {
               
                for (int i = 0; i < activObjects.Count; ++i)
                {
                    
                    Vector3 direction = transform.position - activObjects[i].transform.position;
                    Vector3 force = direction.normalized * power; //Pour stabiliser la force d'attirances
                    RB = activObjects[i].GetComponent<Rigidbody>();
                    RB.AddForce(force, ForceMode.Force); 

                    var tailleObject = activObjects[i].GetComponent<Collider>().bounds.size.x + activObjects[i].GetComponent<Collider>().bounds.size.z; //C'était au cas ou pour faire un truc avec différentes taille de l'objet quand elle arrive vers le joueur

                    if (direction.magnitude < 3) //Lorsque l'objet attiré arrive proche du joueur, celui ci s'active et gravite autour du joueur via un autre script
                    {
                        RB.velocity = Vector3.zero;
                        activObjects[i].rotation = Quaternion.identity;
                        RB.constraints = RigidbodyConstraints.FreezeRotation; //Pour éviter qu'il aille n'importe ou n'importe comment
                        activObjects[i].parent = transform;
                        activObjects[i].GetComponent<turnAround>().isNextTo = true;
                        gravObjects.Add(activObjects[i]);
                        activObjects.Remove(activObjects[i]);
                    }
                 

                }
            }
        }
    }
}
