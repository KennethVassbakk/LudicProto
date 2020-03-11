using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class FroggerPeople : MonoBehaviour
{
    private NavMeshAgent thisAgent;
    private Animator theAnim;
    public List<GameObject> PeopleSkins;
    public GameObject[] Exits;
    private Vector3 myExit;

    private GameObject theActiveOne;

    public bool go;
    public bool PlayerGoal;
    private bool playerInteracted;
    private float storedCounter;

    [Header("Dialogue Options")]
    public TextMeshPro PeopleDialogue;
    public List<string> NegativeResponst;
    public string KindAnswer;
    public string GiveComment;

    private bool GaveText;

    private float counter;

    private void Start()
    {
        theActiveOne = PeopleSkins[Random.Range(0, PeopleSkins.Count)];
        theActiveOne.SetActive(true);
        PeopleDialogue.transform.SetParent(null);
        theAnim = theActiveOne.GetComponent<Animator>();
        thisAgent = GetComponent<NavMeshAgent>();


        Exits = GameObject.FindGameObjectsWithTag("Exits");
        Debug.Log(Exits.Length);
    }

    public void Update()
    {
        if (go == true)
        {
            PeopleDialogue.transform.rotation.SetLookRotation(PeopleDialogue.transform.position - Camera.main.transform.position);
            thisAgent.isStopped = true;
            counter -= Time.deltaTime;
            if (counter < storedCounter)
            {
                if (PlayerGoal == true && GaveText == false)
                {
                    PeopleDialogue.text = KindAnswer;
                    GaveText = true;
                }
                else if (GaveText == false)
                {
                    PeopleDialogue.text = NegativeResponst[Random.Range(0, NegativeResponst.Count - 1)];
                    GaveText = true;
                }
                theAnim.SetTrigger("Interaction");
            }

            if(counter < 0f)
            {
                if (PlayerGoal == true && playerInteracted == true)
                {
                    theAnim.SetTrigger("GotItem");
                    PeopleDialogue.text = GiveComment;
                    PlayerGoal = false;
                    playerInteracted = false;
                    counter = 2.8f;
                    return;
                }
                theAnim.SetTrigger("Interaction");
                PeopleDialogue.text = "";
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
        PeopleDialogue.GetComponent<CameraController>().target = transform;
        playerInteracted = playerInter;
        counter = Length * 2f;
        storedCounter = Length;
        go = true;
        thisAgent.isStopped = true;
    }
}
