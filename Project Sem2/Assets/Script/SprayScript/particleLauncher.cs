﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using UnityStandardAssets.Characters.FirstPerson;


public class particleLauncher : MonoBehaviour
{


    [EventRef]
    public string EventTir = "";
    [EventRef]
    public string EventRelacher = "";
    [EventRef]
    public string EventRecharger = "";

    FMOD.Studio.EventInstance tir;
    FMOD.Studio.EventInstance recharger;

    public ParticleSystem ParticleLauncher;
    public ParticleSystem splatterParticles;
    public Gradient particleColorGradient;
    public Gradient particleColorGradientBlue;
    public Gradient particleColorGradientRed;
    public Gradient particleColorGradientGreen;
    public ParticleDecalPool splatDecalPool;

    List<ParticleCollisionEvent> collisionEvents;

    public Slider tagUIRed;
    public Slider tagUIBlue;
    public Slider tagUIGreen;

    [HideInInspector] public Slider tagUI;

    [HideInInspector] public bool isCharging;

    [SerializeField] private int TimeChargement;

    [SerializeField] private RigidbodyFirstPersonController rbfps;

    void Start()
    {

        collisionEvents = new List<ParticleCollisionEvent>();

        tagUIBlue.value = tagUIBlue.maxValue;
        tagUIGreen.value = tagUIGreen.maxValue;
        tagUIRed.value = tagUIRed.maxValue;

        tagUI = tagUIRed;


        tir = FMODUnity.RuntimeManager.CreateInstance(EventTir);
        recharger = FMODUnity.RuntimeManager.CreateInstance(EventRecharger);


    }

    void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(ParticleLauncher, other, collisionEvents);

        for (int i = 0; i < collisionEvents.Count; i++)
        {
            splatDecalPool.ParticleHit(collisionEvents[i], particleColorGradient);
            //EmitAtLocation(collisionEvents[i]);
        }
        
    }

    /*void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        splatterParticles.transform.position = particleCollisionEvent.intersection; // worldspace
        splatterParticles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        ParticleSystem.MainModule psMain = splatterParticles.main;
        psMain.startColor = particleColorGradient.Evaluate(Random.Range(0f, 1f));

        splatterParticles.Emit(1);
    }*/


    void Update()
    {

        if (Input.GetButton("Fire1") && !isCharging && !splatDecalPool.isChanging)
        {

            rbfps.anim.SetBool("IsCharging", false); //AnimCharging
            ParticleSystem.MainModule psMain;
            switch (splatDecalPool.colorNow)
            {
                case ParticleDecalPool.Color.Red:

                    psMain = ParticleLauncher.main;
                    psMain.startColor = particleColorGradientRed.Evaluate(1f);
                    ParticleLauncher.Emit(1);
                    tagUIRed.value--;


                    break;
                case ParticleDecalPool.Color.Blue:

                    psMain = ParticleLauncher.main;
                    psMain.startColor = particleColorGradientBlue.Evaluate(1f);
                    ParticleLauncher.Emit(1);
                    tagUIBlue.value--;


                    break;
                case ParticleDecalPool.Color.Green:

                    psMain = ParticleLauncher.main;
                    psMain.startColor = particleColorGradientGreen.Evaluate(1f);
                    ParticleLauncher.Emit(1);
                    tagUIGreen.value--;

                    break;

            }

        }
        else if (Input.GetButtonUp("Fire1"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(EventRelacher, transform.position);
            tir.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }


        if (tagUI.value <= 0)
        {
            isCharging = true;
            FMODUnity.RuntimeManager.PlayOneShot(EventRelacher, transform.position);
            tir.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            recharger.start();
            rbfps.anim.SetBool("IsCharging", true); //AnimCharging

        }
        else if (Input.GetKeyDown(KeyCode.R) && tagUI.value < tagUI.maxValue)
        {
            isCharging = true;
            tir.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            recharger.start();
            rbfps.anim.SetBool("IsCharging", true); //AnimCharging
        }

        if (isCharging)
        {           

            if (Input.GetButtonDown("Fire1") && tagUI.value > tagUI.maxValue / 2)
            {
                isCharging = false;
                recharger.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }

            switch (splatDecalPool.colorNow)
            {
                case ParticleDecalPool.Color.Red:
                    tagUIRed.value += TimeChargement * Time.deltaTime;

                    if (tagUIRed.value >= tagUIRed.maxValue)
                    {
                        recharger.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                        isCharging = false;
                        rbfps.anim.SetBool("IsCharging", false); //AnimCharging

                    }

                    break;
                case ParticleDecalPool.Color.Blue:
                    tagUIBlue.value += TimeChargement * Time.deltaTime;

                    if (tagUIBlue.value >= tagUIBlue.maxValue)
                    {
                        recharger.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                        isCharging = false;
                        rbfps.anim.SetBool("IsCharging", false); //AnimCharging

                    }

                    break;
                case ParticleDecalPool.Color.Green:
                    tagUIGreen.value += TimeChargement * Time.deltaTime;

                    if (tagUIGreen.value >= tagUIGreen.maxValue)
                    {
                        recharger.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                        isCharging = false;
                        rbfps.anim.SetBool("IsCharging", false); //AnimCharging

                    }

                    break;
            }

        }

        if (Input.GetButtonDown("Fire1") && !splatDecalPool.isChanging && !isCharging)
        {
            tir.start();
        }
    }
}
