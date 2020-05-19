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

    //[SerializeField] private tagScore tS;
    [SerializeField] private Text text;

    [SerializeField] private GameObject TagRed;
    [SerializeField] private GameObject TagBlue;
    [SerializeField] private GameObject TagGreen;

    public int score = 0;

    public bool isOnParkour;

    [FMODUnity.EventRef]
    public string EventChgtColor;

    public enum Color
    {       
        Red,
        Blue,
        Green
    }

    public Color colorNow = Color.Red;

    public void OnEnterColor()
    {
        Debug.Log("joue le son");
        FMODUnity.RuntimeManager.PlayOneShot(EventChgtColor, transform.position);
        switch (colorNow)
        {
            case Color.Red:
                TagRed.SetActive(true);
                break;
            case Color.Blue:
                TagBlue.SetActive(true);
                break;
            case Color.Green:
                TagGreen.SetActive(true);
                break;

        }
    }

    public void OnExitColor()
    {
        switch (colorNow)
        {
            case Color.Red:
                TagRed.SetActive(false);

                break;
            case Color.Blue:
                TagBlue.SetActive(false);

                break;
            case Color.Green:
                TagGreen.SetActive(false);

                break;

        }
    }

    public void UpdateColor()
    {
        switch (colorNow)
        {
            case Color.Red:

                break;
            case Color.Blue:

                break;
            case Color.Green:

                break;

        }
    }

    public void SwitchColor(Color newColor)
    {
        OnExitColor();
        colorNow = newColor;
        OnEnterColor();

    }


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

        /*if (tS.isScoring == true)
        {
            score++;
            text.text = score.ToString();
        }*/
        particleData[particleDecalDataIndex].position = particleCollisionEvent.intersection;
        Vector3 particleRotationEuler = Quaternion.LookRotation(particleCollisionEvent.normal).eulerAngles;
        particleRotationEuler.z = Random.Range(0, 360);
        particleData[particleDecalDataIndex].rotation = particleRotationEuler;
        particleData[particleDecalDataIndex].size = Random.Range(decalSizeMin, decalSizeMax);
        //particleData[particleDecalDataIndex].color = colorGradient.Evaluate(Random.Range(0f,1f));
        particleDecalDataIndex++;
        Debug.Log(score);
        
        if (colorNow == Color.Red)
        {
            particleData[particleDecalDataIndex].color = colorGradient.Evaluate(0f);
        }
        if (colorNow == Color.Blue)
        {
            particleData[particleDecalDataIndex].color = colorGradient.Evaluate(0.5f);
        }
        if (colorNow == Color.Green)
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

        //UpdateColor();

        if (Input.GetAxis("Mouse ScrollWheel") > 0.1)
        {
            switch (colorNow)
            {
                case Color.Red:
                    SwitchColor(Color.Blue);
                    break;
                case Color.Blue:
                    SwitchColor(Color.Green);

                    break;
                case Color.Green:
                    SwitchColor(Color.Red);
                    break;

            }
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < -0.1)
        {
            switch (colorNow)
            {
                case Color.Red:
                    SwitchColor(Color.Green);
                    break;
                case Color.Blue:
                    SwitchColor(Color.Red);

                    break;
                case Color.Green:
                    SwitchColor(Color.Blue);
                    break;

            }
        }
      

        /*if (tS.isScoring == true)
        {
            text.enabled = true;
        }
        else
        {
            text.enabled = false;
        }*/
    }
}
