using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FroggerTrafficRedirect : MonoBehaviour
{
    public GameObject nextWaypoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Traffic"))
        {
            other.transform.parent.GetComponent<NavMeshAgent>().SetDestination(nextWaypoint.transform.position);
        }
    }
}
