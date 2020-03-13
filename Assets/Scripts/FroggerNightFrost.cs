﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerNightFrost : MonoBehaviour
{
    Rigidbody theRb;

    public List<Light> Lights;
    private bool haveLights;
    public GameObject Midlvl;

    private bool Arrived;

    private void Start()
    {
        theRb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Arrived == false)
        {
            theRb.MovePosition(transform.position + transform.right * Time.deltaTime);

            if (transform.position.x > Midlvl.transform.position.x)
            {
                Arrived = true;
            }
            if (transform.position.z > Midlvl.transform.position.z)
            {
                Arrived = true;
            }

        }
        

        if (haveLights == true)
        {
            DimLights();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tourch"))
        {
            other.GetComponentInChildren<ParticleSystem>().Stop();
            other.GetComponentInChildren<FroggerHotSpot>().HeatValue = 0f;
            Light[] lights = other.GetComponentsInChildren<Light>();

            foreach (Light i in lights)
            {
                if (i.intensity == 0f)
                {
                    return;
                }
                Lights.Add(i);
                haveLights = true;
            }

            
        }
    }

    private void DimLights()
    {
        foreach (Light i in Lights)
        {
            i.intensity = Mathf.Lerp(i.intensity, 0f, Time.deltaTime / 2);

            if (i.intensity < 0.1f)
            {
                i.intensity = 0f;
                Lights.Remove(i);
                if (Lights.Count < 0f)
                {
                    haveLights = false;
                }
            }
        }
    
    }

}