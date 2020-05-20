using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class particleLauncher : MonoBehaviour
{


    [EventRef]
    public string EventTir = "";
    [EventRef]
    public string EventRelacher = "";

    FMOD.Studio.EventInstance tir;


    public ParticleSystem ParticleLauncher;
    public ParticleSystem splatterParticles;
    public Gradient particleColorGradient;
    public Gradient particleColorGradientBlue;
    public Gradient particleColorGradientRed;
    public Gradient particleColorGradientGreen;
    public ParticleDecalPool splatDecalPool;

    List<ParticleCollisionEvent> collisionEvents;

    public Slider tagUI;
    [SerializeField] private float chargeMax;

    [HideInInspector] public bool isCharging;

    [SerializeField] private int TimeChargement;

    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        tagUI.maxValue = chargeMax;
        tagUI.value = chargeMax;

        tir = FMODUnity.RuntimeManager.CreateInstance(EventTir);

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

        if (Input.GetButtonDown("Fire1"))
        {
            tir.start();
        }

        if (Input.GetButton("Fire1") && !isCharging)
        {

            ParticleSystem.MainModule psMain;
            switch (splatDecalPool.colorNow)
            {
                case ParticleDecalPool.Color.Red:

                    psMain = ParticleLauncher.main;
                    psMain.startColor = particleColorGradientRed.Evaluate(1f);
                    ParticleLauncher.Emit(1);

                    break;
                case ParticleDecalPool.Color.Blue:

                    psMain = ParticleLauncher.main;
                    psMain.startColor = particleColorGradientBlue.Evaluate(1f);
                    ParticleLauncher.Emit(1);

                    break;
                case ParticleDecalPool.Color.Green:

                    psMain = ParticleLauncher.main;
                    psMain.startColor = particleColorGradientGreen.Evaluate(1f);
                    ParticleLauncher.Emit(1);

                    break;

            }

            tagUI.value--;

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

        }
        else if (Input.GetKeyDown(KeyCode.R) && tagUI.value < chargeMax)
        {
            isCharging = true;
            tir.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

        if (isCharging)
        {
            tagUI.value += TimeChargement * Time.deltaTime;

            if(tagUI.value >= chargeMax)
            {
                isCharging = false;
            }
            else if (Input.GetButtonDown("Fire1") && tagUI.value > 50)
            {
                isCharging = false;
                tir.start();
            }
        }

        /*if (Input.GetButton("Fire1") && splatDecalPool.Red == true)
        {
            ParticleSystem.MainModule psMain = ParticleLauncher.main;
            psMain.startColor = particleColorGradientRed.Evaluate(1f);
            ParticleLauncher.Emit(1);
        }

        if (Input.GetButton("Fire1") && splatDecalPool.Blue == true)
        {
            ParticleSystem.MainModule psMain = ParticleLauncher.main;
            psMain.startColor = particleColorGradientBlue.Evaluate(1f);
            ParticleLauncher.Emit(1);
        }

        if (Input.GetButton("Fire1") && splatDecalPool.Green == true)
        {
            ParticleSystem.MainModule psMain = ParticleLauncher.main;
            psMain.startColor = particleColorGradientGreen.Evaluate(1f);
            ParticleLauncher.Emit(1);
        }*/
    }
}
