using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class patrol : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private List<Transform> waypoints;

    [HideInInspector] public int cible = 0;

    private int increment = 1;

    [SerializeField] private bool AlléRetour;

    [SerializeField] private int timeWait;
    private float chrono;

    public bool wait;

    public enum State
    {
        Idle,
        Patrol
    }

    public State state = State.Idle;

    public void SwitchState(State newState)
    {
        OnExitState();
        state = newState;
        OnEnterState();
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

                    Transform target = waypoints[cible];
                    Vector3 direction = waypoints[cible].position - transform.position;
                    agent.SetDestination(target.position);

                    if (direction.magnitude < 3.1)
                    {
                        
                        if(Random.value >= 0.7 && !wait)
                        {
                            wait = true;                          
                        }
                        else if(Random.value < 0.7 && !wait)
                        {
                            cible += increment;
                        }

                        if (wait)
                        {
                            chrono += Time.deltaTime;
                            if (chrono >= timeWait)
                            {
                                cible += increment;
                                chrono = 0;
                                wait = false;
                            }
                        }
                    }

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

        }
    }
}
