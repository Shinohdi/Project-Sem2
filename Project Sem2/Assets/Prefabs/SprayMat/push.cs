using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class push : MonoBehaviour
{
    public Animator Animator;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Animator.SetBool("bool", true);
        }
        else
        {
            Animator.SetBool("bool", false);
        }

    }
}
