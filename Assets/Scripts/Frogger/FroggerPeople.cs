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

    private GameObject player;
    private GameObject theActiveOne;
    public GameObject Scarf;

    public bool go;
    public bool PlayerGoal;
    private bool playerInteracted;
    private bool speak;
    private float storedCounter;

    [Header("Dialogue Options")]
    public TextMeshPro PeopleDialogue;
    [TextArea(5, 10)]
    public List<string> NegativeResponst;
    public string KindAnswer;
    public string GiveComment;

    private bool GaveText;

    private float counter;

    private float turnSpeed = 2f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        theActiveOne = PeopleSkins[Random.Range(0, PeopleSkins.Count - 1)];
        theActiveOne.SetActive(true);
        theAnim = theActiveOne.GetComponent<Animator>();
        thisAgent = GetComponent<NavMeshAgent>();


        Exits = GameObject.FindGameObjectsWithTag("Exits");
        myExit = Exits[Random.Range(0, Exits.Length - 1)].transform.position;

    }

    public void Update()
    {
        if (go == true)
        {
            if (speak == true)
            {
                Vector3 targetFix = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                Vector3 targetDir = targetFix - transform.position;

                float singleStep = turnSpeed * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, singleStep, 0.0f);

                transform.rotation = Quaternion.LookRotation(newDir);
            }
            thisAgent.isStopped = true;
            counter -= Time.deltaTime;
            if (counter < storedCounter && GaveText == false && speak == true)
            {
                PeopleDialogue.transform.rotation.SetLookRotation(PeopleDialogue.transform.position - Camera.main.transform.position);
                PeopleDialogue.transform.position = transform.position + new Vector3(0f, 2f, 0f);
                PeopleDialogue.GetComponent<CameraController>().target = transform;
                if (PlayerGoal == true)
                {
                    PeopleDialogue.text = KindAnswer;
                    GaveText = true;
                }
                else
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
                    GameProperties.HaveScarf = 1;
                    Scarf.SetActive(true);
                    theAnim.SetTrigger("GotItem");
                    PeopleDialogue.text = GiveComment;
                    PlayerGoal = false;
                    playerInteracted = false;
                    counter = 2.8f;
                    return;
                }
                theAnim.SetBool("Idle", false);
                theAnim.SetBool("Walk", true);
                go = false;
                
                thisAgent.SetDestination(myExit);
                if (GaveText == true)
                {
                    PeopleDialogue.text = "";
                    speak = false;
                }
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
        if (theAnim)
        {
            theAnim.SetBool("Idle", true);
            theAnim.SetBool("Walk", false);
        }
        if (playerInter == false && speak == true)
        {
            return;
        }
        playerInteracted = playerInter;
        speak = playerInter;
        counter = Length * 2f;
        storedCounter = Length - 0.1f;
        go = true;
        thisAgent.isStopped = true;
    }
}
