using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerLevelManager : MonoBehaviour
{
    //public GameObject[] People;
    public List<GameObject> peopleList;
    private GameObject player;
    float LeaveInterval;
    float leavetrigger;

    //The Sunlight
    [Header("Sunlight Options")]
    public Light TheSun;
    public Color DayColor;
    public Color NightColor;
    public float LevelTimeMinutes;

    private float counter;
    private float t = 0f;

    [Header("The Fog")]
    public GameObject fog1;
    public GameObject fog2;

    public Animator cart;


    void Start()
    {
        //Set Sec To Min
        LevelTimeMinutes *= 60;
        counter = LevelTimeMinutes;

        // Give a random person in the scene the objective.
        GameObject[] People = GameObject.FindGameObjectsWithTag("Person");
        foreach (GameObject i in People)
        {
            peopleList.Add(i);
        }
        player = GameObject.FindGameObjectWithTag("Player");
        peopleList[Random.Range(0, peopleList.Count - 1)].GetComponentInParent<FroggerPeople>().PlayerGoal = true;

        LeaveInterval = LevelTimeMinutes / People.Length;
        leavetrigger = LevelTimeMinutes - LeaveInterval;
        
    }


    void Update()
    {
        counter -= Time.deltaTime;

        if (counter < leavetrigger && counter > 0f)
        {
            if (peopleList.Count - 1 > 0f)
            {
                GameObject person = peopleList[Random.Range(0, peopleList.Count)];
                person.GetComponentInParent<FroggerPeople>().Interact(0f, false);
                peopleList.Remove(person);
                leavetrigger = counter - LeaveInterval + 1f;
            }

        }

        if (counter < 10f && counter > 9f)
        {
            player.GetComponent<FroggerPlayer>().Dialogue("It's getting late, I better head home", 5f);
        }

         if (counter < 0f)
        {
            fog1.SetActive(true);
            fog2.SetActive(true);
            cart.SetTrigger("Move");
        }

        //Lerp Sun Color for the day's duration
        TheSun.color = Color.Lerp(DayColor, NightColor, t);
        t += Time.deltaTime / LevelTimeMinutes;
    }


    public void Changescene()
    {

    }
}
