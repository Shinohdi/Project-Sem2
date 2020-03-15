using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBoule : MonoBehaviour
{
    [SerializeField] private GameObject peintureRouge;
    [SerializeField] private GameObject peintureBleu;

    public enum stateBoule
    {
        SansPeinture,
        PeintureRouge,
        PeintureBleu
    }

    public stateBoule state = stateBoule.SansPeinture;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (state)
        {
            case stateBoule.PeintureRouge:
                if (collision.gameObject.CompareTag("Sol"))
                {
                    Instantiate(peintureRouge, new Vector3(transform.position.x, 0.09f, transform.position.z), Quaternion.identity);
                }
                break;

            case stateBoule.PeintureBleu:
                if (collision.gameObject.CompareTag("Sol"))
                {
                    Instantiate(peintureBleu, new Vector3(transform.position.x, 0.09f, transform.position.z), Quaternion.identity);
                }
                break;
        }
    }
}
