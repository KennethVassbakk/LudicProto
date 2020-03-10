using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerLevelManager : MonoBehaviour
{
    GameObject[] People;
    float LeaveInterval;
    float leavetrigger;
    int personToLeave = 0;

    //The Sunlight
    [Header("Sunlight Options")]
    public Light TheSun;
    public Color DayColor;
    public Color NightColor;
    public float LevelTimeMinutes;

    private float counter;
    private float t = 0f;


    void Start()
    {
        //Set Sec To Min
        LevelTimeMinutes *= 60;
        counter = LevelTimeMinutes;

        // Give a random person in the scene the objective.
        People = GameObject.FindGameObjectsWithTag("Person");
        Debug.Log(People.Length + "People in the Scene");
        People[Random.Range(0, People.Length - 1)].GetComponentInParent<FroggerPeople>().PlayerGoal = true;

        LeaveInterval = LevelTimeMinutes / People.Length;
        leavetrigger = LevelTimeMinutes - LeaveInterval;
        
    }


    void Update()
    {
        counter -= Time.deltaTime;

        if (counter < leavetrigger && personToLeave < People.Length)
        {
            Debug.Log("PersonLeft");
            People[personToLeave].GetComponentInParent<FroggerPeople>().Interact(0f, false);
            personToLeave += 1;
            leavetrigger = counter - LeaveInterval;
        }

        //Lerp Sun Color for the day's duration
        TheSun.color = Color.Lerp(DayColor, NightColor, t);
        t += Time.deltaTime / LevelTimeMinutes;
    }


    public void Changescene()
    {

    }
}
