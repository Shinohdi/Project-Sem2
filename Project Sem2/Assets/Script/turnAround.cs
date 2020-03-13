using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnAround : MonoBehaviour
{

    public bool isNextTo = false;

    [SerializeField] private int vitesse;

    [SerializeField] private Transform target;

    private Rigidbody RB;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isNextTo == true)
        {
            transform.RotateAround(target.position, Vector3.up, vitesse * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isNextTo == false)
        {
            if(collision.gameObject.CompareTag("Mur"))
            {
                Vector3 normalWall = collision.GetContact(0).normal;

                Vector3 velocity = new Vector3(RB.velocity.x, 0, RB.velocity.z);
                Vector3 dirReflect = Vector3.Reflect(transform.position, normalWall).normalized;

                RB.velocity = dirReflect * 25;
            }
        }
    }
}
