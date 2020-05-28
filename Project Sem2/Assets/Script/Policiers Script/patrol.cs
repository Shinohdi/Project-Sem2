using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FMODUnity;

public class patrol : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private Transform player;

    [HideInInspector] public int cible = 0;

    private int increment = 1;

    private Transform target;

    [SerializeField] private bool AlléRetour;

    [SerializeField] private int timeWait;
    [SerializeField] private int timeBlind;
    [SerializeField] private float rangeView;

    private float rangeViewPatrol;
    private float rangeViewLook;

    private float chrono;

    [Range(0, 1)] [SerializeField] private float rangeWait;
    [Range(-1, 1)] private float viewAround;


    public Animator animFlic; //Animation

    [EventRef]
    public string EventEtonnement = "";

    [EventRef]
    public string EventDamaged = "";

    public enum State
    {
        Idle,
        Patrol,
        Look,
        Chase,
        Blind
    }

    public State state = State.Idle;

    public void SwitchState(State newState)
    {
        OnExitState();
        state = newState;
        OnEnterState();
    }

    private void Start()
    {
        rangeViewPatrol = rangeView;
        rangeViewLook = rangeView * 1.5f;
    }

    private void Update()
    {
        UpdateState();
        //Debug.Log(agent.remainingDistance);
    }

    private void OnEnterState()
    {
        switch (state)
        {
            case State.Look:               
                animFlic.SetBool("IsIdle", true); //AnimFlic
                chrono = 0;
                viewAround = -0.5f;
                rangeView = rangeViewLook;
                agent.isStopped = true;
                break;

            case State.Chase:
                target = player;
                FMODUnity.RuntimeManager.PlayOneShot(EventEtonnement, target.position);
                agent.isStopped = false;
                agent.speed = 10;
                break;

            case State.Patrol:
                agent.speed = 7;
                viewAround = 0.5f;
                rangeView = rangeViewPatrol;
                agent.isStopped = false;
                break;

            case State.Blind:
                FMODUnity.RuntimeManager.PlayOneShot(EventDamaged, transform.position);
                animFlic.SetBool("IsSplat", true); //AnimFlic
                agent.isStopped = true;
                break;

        }
    }


    

    private void UpdateState()
    {
        switch (state)
        {
            case State.Idle:
                SwitchState(State.Patrol);
                break;

            case State.Patrol:
                if(waypoints.Count > 0)
                {
                    TargetID();

                    target = waypoints[cible];
                    Vector3 direction = waypoints[cible].position - transform.position;
                    agent.SetDestination(target.position);

                    SeeThePlayer();
                    if (direction.magnitude < 3.1)
                    {
                        
                        if(Random.value >= rangeWait && state != State.Look)
                        {
                            SwitchState(State.Look);                         
                        }
                        else
                        {
                            cible += increment;
                        }

                    }

                }
                break;

            case State.Look:
                SeeThePlayer();
                chrono += Time.deltaTime;
                if (chrono >= timeWait)
                {
                    cible += increment;
                    chrono = 0;
                    SwitchState(State.Patrol);
                }
                break;

            case State.Chase:
                agent.SetDestination(target.position);
                Vector3 directionChase = target.position - transform.position;

                if (directionChase.magnitude > rangeView * 2)
                {
                    SwitchState(State.Patrol);
                }
                else if (target.position.y > 11.5f)
                {
                    SwitchState(State.Patrol);

                }
                else
                {
                    transform.LookAt(target);

                }
                break;

            case State.Blind:
                chrono += Time.deltaTime;

                if(chrono >= timeBlind)
                {
                    animFlic.SetBool("IsSplat", false); //AnimFlic
                    SwitchState(State.Look);               
                }

                break;
        }
    }

    private void TargetID()
    {
        if (cible >= waypoints.Count)
        {
            if (AlléRetour)
            {
                increment = -1;
                cible = waypoints.Count - 1;
            }
            else
            {
                cible = 0;
            }
        }
        else if (cible <= 0)
        {
            if (AlléRetour)
            {
                increment = 1;

            }

        }
    }

    private void OnExitState()
    {
        switch (state)
        {
            case State.Look:
                animFlic.SetBool("IsIdle", false); //AnimTagOnCorniche
                break;
        }
    }

    private void SeeThePlayer()
    {
        Vector3 playerDirection = player.position - transform.position;
        float dist = playerDirection.magnitude;

        if(dist < rangeView)
        {
            playerDirection = playerDirection.normalized;

            Debug.DrawRay(transform.position, playerDirection, Color.blue);
            Debug.DrawRay(transform.position, transform.forward, Color.magenta);

            float dot = Vector3.Dot(playerDirection, transform.forward);
            
            if(dot > viewAround)
            {
                SwitchState(State.Chase);
            }
        }
    }
}
