using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerExitTutorial : MonoBehaviour
{
    public GameObject NewDestination;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<FroggerPlayer>().inControl = false;
            other.GetComponent<FroggerPlayer>().Player.SetDestination(NewDestination.transform.position);
        }
    }
}
