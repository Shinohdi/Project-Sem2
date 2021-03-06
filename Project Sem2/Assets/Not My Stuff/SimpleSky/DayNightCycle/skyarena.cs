﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyarena : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.1f;
    private float offset = 0.5f;
    Renderer rend;

    [SerializeField] private GameObject Sun;

    [SerializeField] private GameObject Moon;

    [SerializeField] private GameObject Stars;

    Color oui;

    void Start()
    {
        rend = GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * scrollSpeed;
        rend.material.mainTextureOffset = new Vector2(offset, 0);

        if(offset >= 0.75)
        {
            Sun.SetActive(true);
            Moon.SetActive(false);
            Stars.SetActive(false);
        }
    }
}
