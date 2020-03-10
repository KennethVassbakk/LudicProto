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

    private bool go;
    private bool movingOut;
    public bool PlayerGoal;


    private float counter;

    private void Start()
    {
        theActiveOne = PeopleSkins[Random.Range(0, PeopleSkins.Count - 1)];
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
            counter -= Time.deltaTime;

            if(counter < 0f)
            {
                if (PlayerGoal == true)
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

        float exitdist = Vector3.Distance(transform.position, myExit);

        if (exitdist < 2f)
        {
            Destroy(transform.gameObject);
        }

    }

    public void Interact(float Length)
    {
        counter = Length;
        theAnim.SetTrigger("Interaction");
        go = true;
    }

}
