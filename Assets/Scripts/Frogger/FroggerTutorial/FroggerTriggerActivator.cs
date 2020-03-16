using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerTriggerActivator : MonoBehaviour
{
    public GameObject StartObject;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartObject.SetActive(true);
            
        }
    }
}
