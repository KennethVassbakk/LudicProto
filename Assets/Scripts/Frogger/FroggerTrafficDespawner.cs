using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerTrafficDespawner : MonoBehaviour
{
    public GameObject nextWaypoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Traffic"))
        {
            PoolManager.Despawn(other.transform.parent.gameObject);
        }
    }
}
