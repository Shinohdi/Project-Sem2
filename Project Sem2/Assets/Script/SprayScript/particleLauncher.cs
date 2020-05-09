using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleLauncher : MonoBehaviour
{
    public ParticleSystem ParticleLauncher;
    public ParticleSystem splatterParticles;
    public Gradient particleColorGradient;
    public Gradient particleColorGradientBlue;
    public Gradient particleColorGradientRed;
    public Gradient particleColorGradientGreen;
    public ParticleDecalPool splatDecalPool;

    List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(ParticleLauncher, other, collisionEvents);

        for (int i = 0; i < collisionEvents.Count; i++)
        {
            splatDecalPool.ParticleHit(collisionEvents[i], particleColorGradient);
            EmitAtLocation(collisionEvents[i]);
        }
        
    }

    void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        splatterParticles.transform.position = particleCollisionEvent.intersection; // worldspace
        splatterParticles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        ParticleSystem.MainModule psMain = splatterParticles.main;
        psMain.startColor = particleColorGradient.Evaluate(Random.Range(0f, 1f));

        splatterParticles.Emit(1);
    }


    void Update()
    {
        if (Input.GetButton("Fire1"))
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
