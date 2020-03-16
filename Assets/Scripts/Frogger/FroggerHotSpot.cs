using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerHotSpot : MonoBehaviour
{
    private float StoredValue;

    public float HeatValue;

    public bool playerInside;

    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInside = true;
            player = other.gameObject;
            other.GetComponent<FroggerPlayer>().Warmth(true, transform.gameObject);
            StoredValue = other.GetComponent<FrostControlFrogger>().FrostToGive;
            other.GetComponent<FrostControlFrogger>().FrostToGive = HeatValue * -1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInside = false;
            other.GetComponent<FroggerPlayer>().Warmth(false, transform.gameObject);
            other.GetComponent<FrostControlFrogger>().FrostToGive = StoredValue;
        }
    }

    public void KillHeat()
    {
        player.GetComponent<FroggerPlayer>().Warmth(false, transform.gameObject);
        player.GetComponent<FrostControlFrogger>().FrostToGive = StoredValue;
    }
}
