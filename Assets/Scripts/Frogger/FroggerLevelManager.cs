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


    private float TutorialCounter;
    private bool tutorialCheck;


    [Header("The Fog")]
    public GameObject fog1;
    public GameObject fog2;

    public Animator cart;

    public bool gameStarted;
    private bool introText;

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
        TheSun.color = DayColor;
    }


    void Update()
    {

        if (gameStarted == true)
        {
            if (introText == false)
            {
                player.GetComponent<FroggerPlayer>().Dialogue("It's getting dark soon, I really hope someone will buy from me...", 4f);
                introText = true;
            }
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
                /*
                GameObject[] TrafficSpawners = GameObject.FindGameObjectsWithTag("Traffic");

                foreach(GameObject i in TrafficSpawners)
                {
                    i.GetComponent<FroggerTraffic>().enabled = false;
                    i.gameObject.tag = "Untagged";
                }
                */
            }

            //Lerp Sun Color for the day's duration
            TheSun.color = Color.Lerp(DayColor, NightColor, t);
            t += Time.deltaTime / LevelTimeMinutes;
        }
        else
        {
            TutorialCounter += Time.deltaTime;

            if (TutorialCounter > 2f)
            {
                if (tutorialCheck == false)
                {
                    player.GetComponent<FroggerPlayer>().Dialogue("it's so cold today, I should warm up by the fire", 4f);
                    tutorialCheck = true;
                }

            }
        }
    }
}
