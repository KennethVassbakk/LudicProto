using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerHotSpot : MonoBehaviour
{
    private float StoredValue;

    public float HeatValue;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Entered2");
            StoredValue = other.GetComponent<FrostControlFrogger>().FrostToGive;
            other.GetComponent<FrostControlFrogger>().FrostToGive = HeatValue * -1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<FrostControlFrogger>().FrostToGive = StoredValue;
        }
        
    }
}
