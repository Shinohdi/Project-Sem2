using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBoule : MonoBehaviour
{
    [SerializeField] private GameObject peintureRouge;
    [SerializeField] private GameObject peintureBleu;

    [SerializeField] private PhysicMaterial Rebond;

    private Collider CL;
    private MeshRenderer MS;


    public enum stateBoule
    {
        SansPeinture,
        PeintureRouge,
        PeintureBleu
    }

    public stateBoule state = stateBoule.SansPeinture;

    public void SwitchState(stateBoule newState)
    {
        OnExitState();
        state = newState;
        OnEnterState();
    }

    private void OnEnterState()
    {
        switch (state)
        {
            case stateBoule.PeintureRouge:
                CL.material = Rebond;
                break;
        }
    }

    private void OnExitState()
    {
        switch (state)
        {
            case stateBoule.PeintureRouge:
                CL.material = null;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CL = GetComponent<Collider>();
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
                    Instantiate(peintureRouge, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
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
