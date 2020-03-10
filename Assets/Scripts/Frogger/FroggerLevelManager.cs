using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerLevelManager : MonoBehaviour
{
    GameObject[] People;

    // Start is called before the first frame update
    void Start()
    {
        People = GameObject.FindGameObjectsWithTag("Person");
        Debug.Log(People.Length + "People in the Scene");

        People[Random.Range(0, People.Length - 1)].GetComponentInParent<FroggerPeople>().PlayerGoal = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
