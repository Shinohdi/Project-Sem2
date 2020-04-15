using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class patrol : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Transform firstPoint;
    [SerializeField] private Transform secondPoint;
    [SerializeField] private Transform thirdPoint;



    public enum State
    {
        Idle,
        firstPatrol,
        secondPatrol,
        thirdPatrol
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
        Debug.Log(agent.remainingDistance);
    }

    private void OnEnterState()
    {
        switch (state)
        {
            case State.firstPatrol:
                agent.SetDestination(firstPoint.position);
                break;

            case State.secondPatrol:
                agent.SetDestination(secondPoint.position);
                break;

            case State.thirdPatrol:
                agent.SetDestination(thirdPoint.position);
                break;

        }
    }

    private void UpdateState()
    {
        switch (state)
        {
            case State.Idle:
                SwitchState(State.firstPatrol);
                break;

            case State.firstPatrol:
                if (agent.remainingDistance < 0.1f)
                {
                    SwitchState(State.secondPatrol);
                }
                break;

            case State.secondPatrol:
                if (agent.remainingDistance < 0.5f)
                {
                    SwitchState(State.thirdPatrol);
                }
                break;

            case State.thirdPatrol:
                if(agent.remainingDistance < 0.5f)
                {
                    SwitchState(State.Idle);
                }
                break;
        }
    }

    private void OnExitState()
    {
        switch (state)
        {

        }
    }
}
