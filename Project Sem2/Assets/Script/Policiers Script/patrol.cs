﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField] private float rangeView;

    private float chrono;

    [Range(0, 1)] [SerializeField] private float rangeWait;

    public Animator animFlic; //Animation

    public enum State
    {
        Idle,
        Patrol,
        Look,
        Chase
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
            case State.Look:
                animFlic.SetBool("IsIdle", true); //AnimFlic
                
                break;

            case State.Chase:
                target = player;
                agent.speed = 10;
                break;

            case State.Patrol:
                agent.speed = 7;
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
                transform.LookAt(target);

                if (directionChase.magnitude > rangeView * 2)
                {
                    SwitchState(State.Patrol);
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
            
            if(dot > 0.5f)
            {
                SwitchState(State.Chase);
            }
        }
    }
}
