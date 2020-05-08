using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParticleDecalPool : MonoBehaviour
{
    public int maxDecals = 100;
    public float decalSizeMin = 0.5f;
    public float decalSizeMax = 1.5f;

    private ParticleSystem decalParticleSystem;
    private particleDecalData[] particleData;
    private int particleDecalDataIndex;
    private ParticleSystem.Particle[] particles;
    public bool Red = true;
    public bool Blue = false;
    public bool Green = false;

    [SerializeField] private tagScore tS;
    [SerializeField] private Text text;


    public int score = 0;

    void Start()
    {
        decalParticleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[maxDecals];
        particleData = new particleDecalData[maxDecals];
        for (int i = 0; i < maxDecals; i++)
        {
            particleData[i] = new particleDecalData();
        }

    }

    public void ParticleHit(ParticleCollisionEvent particleCollisionEvent, Gradient colorGradient)
    {
        SetParticleData(particleCollisionEvent, colorGradient);
        DisplayParticles();
    }

    void SetParticleData(ParticleCollisionEvent particleCollisionEvent, Gradient colorGradient)
    {
        //record collision position, rotation, size and color
        if (particleDecalDataIndex >= maxDecals)
        {
            particleDecalDataIndex = 0;
        }

        if (tS.isScoring == true)
        {
            score++;
            text.text = score.ToString();
        }
        particleData[particleDecalDataIndex].position = particleCollisionEvent.intersection;
        Vector3 particleRotationEuler = Quaternion.LookRotation(particleCollisionEvent.normal).eulerAngles;
        particleRotationEuler.z = Random.Range(0, 360);
        particleData[particleDecalDataIndex].rotation = particleRotationEuler;
        particleData[particleDecalDataIndex].size = Random.Range(decalSizeMin, decalSizeMax);
        //particleData[particleDecalDataIndex].color = colorGradient.Evaluate(Random.Range(0f,1f));
        particleDecalDataIndex++;
        Debug.Log(score);
        
        if (Red == true)
        {
            particleData[particleDecalDataIndex].color = colorGradient.Evaluate(0f);
        }
        if (Blue == true)
        {
            particleData[particleDecalDataIndex].color = colorGradient.Evaluate(0.5f);
        }
        if (Green == true)
        {
            particleData[particleDecalDataIndex].color = colorGradient.Evaluate(1f);
        }
        
    }

    void DisplayParticles()
    {
        for (int i = 0; i < particleData.Length; i++)
        {
            particles[i].position = particleData[i].position;
            particles[i].rotation3D = particleData[i].rotation;
            particles[i].startSize = particleData[i].size;
            particles[i].startColor = particleData[i].color;
        }
        decalParticleSystem.SetParticles(particles, particles.Length);

    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0.1 || Input.GetAxis("Mouse ScrollWheel") < -0.1)
        {
            if(Red == true)
            {
                Blue = true;
                Red = false;
            }
            else if(Blue == true)
            {
                Green = true;
                Blue = false;
            }
            else if(Green == true)
            {
                Red = true;
                Green = false;
            }
        }

        if (tS.isScoring == true)
        {
            text.enabled = true;
        }
        else
        {
            text.enabled = false;
        }
    }
}
