using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ParticleDecalPool : MonoBehaviour
{
    public int maxDecals = 10000;
    public float decalSizeMin = 0.5f;
    public float decalSizeMax = 1.5f;

    private ParticleSystem decalParticleSystem;
    private particleDecalData[] particleData;
    private int particleDecalDataIndex;
    private ParticleSystem.Particle[] particles;

    public List<tagScore> panneau;
    public int PanneauClear;

    [SerializeField] private GameObject TagRed;
    [SerializeField] private GameObject TagBlue;
    [SerializeField] private GameObject TagGreen;

    [SerializeField] private particleLauncher shot;

    [SerializeField] private gameOver gO;


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
                shot.tagUIRed.gameObject.SetActive(true);
                shot.tagUI = shot.tagUIRed;

                break;
            case Color.Blue:
                TagBlue.SetActive(true);
                shot.tagUIBlue.gameObject.SetActive(true);
                shot.tagUI = shot.tagUIBlue;


                break;
            case Color.Green:
                TagGreen.SetActive(true);
                shot.tagUIGreen.gameObject.SetActive(true);
                shot.tagUI = shot.tagUIGreen;


                break;

        }
    }

    public void OnExitColor()
    {
        switch (colorNow)
        {
            case Color.Red:
                TagRed.SetActive(false);
                shot.tagUIRed.gameObject.SetActive(false);              


                break;
            case Color.Blue:
                TagBlue.SetActive(false);
                shot.tagUIBlue.gameObject.SetActive(false);


                break;
            case Color.Green:
                TagGreen.SetActive(false);
                shot.tagUIGreen.gameObject.SetActive(false);


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


        for (int i = 0; i < panneau.Count; i++)
        {
            if (panneau[i].isScoring == true)
            {
                panneau[i].score += panneau[i].multiplicateur;
            }
        }

       

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

        particleData[particleDecalDataIndex].position = particleCollisionEvent.intersection;
        Vector3 particleRotationEuler = Quaternion.LookRotation(particleCollisionEvent.normal).eulerAngles;
        particleRotationEuler.z = Random.Range(0, 360);
        particleData[particleDecalDataIndex].rotation = particleRotationEuler;
        particleData[particleDecalDataIndex].size = Random.Range(decalSizeMin, decalSizeMax);
        //particleData[particleDecalDataIndex].color = colorGradient.Evaluate(Random.Range(0f,1f));

        if(particleDecalDataIndex > 0)
        {
            if (particleData[particleDecalDataIndex].position != particleData[particleDecalDataIndex - 1].position)
            {
                //Debug.Log("no");
                for (int i = 0; i < panneau.Count; i++)
                {
                    if (panneau[i].isScoring == true)
                    {
                        panneau[i].score += panneau[i].multiplicateur;
                    }
                }
            }
        }

        else
        {
            //Debug.Log("yes");
            for (int i = 0; i < panneau.Count; i++)
            {
                if (panneau[i].isScoring == true)
                {
                    panneau[i].score += panneau[i].multiplicateur;
                }
            }
        }

        particleDecalDataIndex++;

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

        if(panneau.Count > 0)
        {
            for (int i = 0; i < panneau.Count; i++)
            {
                if (panneau[i].Completed)
                {
                    panneau.Remove(panneau[i]);
                    PanneauClear++;
                }
            }
        }
        else
        {
            gO.gameOverBool = true;
        }
        
      
    }
}
