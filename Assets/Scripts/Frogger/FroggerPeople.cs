using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FroggerPeople : MonoBehaviour
{
    private NavMeshAgent thisAgent;
    private Animator theAnim;
    public List<GameObject> PeopleSkins;
    public GameObject[] Exits;
    private Vector3 myExit;

    private GameObject theActiveOne;

    public bool go;
    private bool movingOut;
    public bool PlayerGoal;
    private bool playerInteracted;


    private float counter;

    private void Start()
    {
        theActiveOne = PeopleSkins[Random.Range(0, PeopleSkins.Count)];
        theActiveOne.SetActive(true);
        theAnim = theActiveOne.GetComponent<Animator>();
        thisAgent = GetComponent<NavMeshAgent>();

        Exits = GameObject.FindGameObjectsWithTag("Exits");
        Debug.Log(Exits.Length);
    }

    public void Update()
    {
        if (go == true)
        {
            thisAgent.isStopped = true;
            counter -= Time.deltaTime;

            if(counter < 0f)
            {
                if (PlayerGoal == true && playerInteracted == true)
                {
                    theAnim.SetTrigger("GotItem");
                    PlayerGoal = false;
                    counter = 2.8f;
                    return;
                }
                theAnim.SetTrigger("Interaction");

                go = false;
                myExit = Exits[Random.Range(0, Exits.Length - 1)].transform.position;
                thisAgent.SetDestination(myExit);
                return;
            }
        }
        else
        {
            thisAgent.isStopped = false;
        }

        float exitdist = Vector3.Distance(transform.position, myExit);

        if (exitdist < 2f)
        {
            transform.gameObject.SetActive(false);
        }

    }

    public void Interact(float Length, bool playerInter)
    {
        playerInteracted = playerInter;
        counter = Length;
        thisAgent.isStopped = true;
        theAnim.SetTrigger("Interaction");
        go = true;
    }
}
