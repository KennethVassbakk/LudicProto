using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FroggerTraffic : MonoBehaviour
{
    public GameObject StartPoint;
    public GameObject Waypoint;

    public List<GameObject> TrafficObjects;

    public float MaxTime;
    public float MinTime;
    private float counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = Random.Range(MinTime, MaxTime);
        SpawnTraffic();
    }

    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;

        if(counter < 0)
        {
            SpawnTraffic();
            counter = Random.Range(MinTime, MaxTime);
        }
    }

    private void SpawnTraffic()
    {
        GameObject spawnedObj = PoolManager.Spawn(TrafficObjects[Random.Range(0, TrafficObjects.Count - 1)], StartPoint.transform.position, StartPoint.transform.rotation);
        spawnedObj.GetComponent<NavMeshAgent>().SetDestination(Waypoint.transform.position);
    }
}
