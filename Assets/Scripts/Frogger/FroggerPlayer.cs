/**
 *  Author: John Hauge
 *  Frogger Movement Script
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class FroggerPlayer : MonoBehaviour
{
    private NavMeshAgent Player;
    private Vector3 Destination;
    private float ClickDetect;
    private bool click;

    public GameObject MoveIndicator;
    private float counter;

    PlayerInput inputLocation;
    PlayerInput inputAction;

    private bool warmingUp;
    private GameObject Tourch;
    private float turnSpeed = 2f;

    //Try to sell
    public float SaleTime;
    public List<string> SaleLines;
    public TextMeshPro textObj;

    private bool selling;
    private GameObject Person;


    //Animation
    private Vector3 previousPosition;
    public float curSpeed;
    public Animator theAnim;
    private float prevWeight;

    private void Awake()
    {
        inputAction = new PlayerInput();
        inputLocation = new PlayerInput();

        inputAction.PlayerControls.TouchDetect.performed += ctx => ClickDetect = ctx.ReadValue<float>();
        inputAction.PlayerControls.TouchMove.performed += ctx => Destination = ctx.ReadValue<Vector2>();
    }

    // Start is called before the first frame update
    void Start()
    {
        prevWeight = theAnim.GetLayerWeight(1);
        Player = GetComponent<NavMeshAgent>();
        selling = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curMove = transform.position - previousPosition;
        curSpeed = curMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;

        theAnim.SetFloat("Speed", curSpeed);



        if (ClickDetect > 0.1f && selling == false)
        {


            Player.isStopped = false;
            Ray castPoint = Camera.main.ScreenPointToRay(Destination);

            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                if (click == false)
                {
                    click = true;
                    MoveIndicator.transform.position = hit.point;
                    MoveIndicator.SetActive(true);
                }

                if (hit.transform.gameObject.CompareTag("Person"))
                {
                    MoveIndicator.SetActive(false);
                    Vector3 Direction = transform.position - hit.transform.position;

                    float dist = Direction.magnitude;
                    if (dist < 3f)
                    {
                        Person = hit.transform.gameObject;
                        counter = SaleTime;
                        textObj.text = SaleLines[Random.Range(0, SaleLines.Count - 1)];
                        selling = true;
                        hit.transform.parent.GetComponent<FroggerPeople>().Interact(SaleTime, true);
                        return;
                    }
                }

                Player.SetDestination(hit.point);
            }
        }
        else
        {
            click = false;
        }

        if (selling == true)
        {
            
            Player.isStopped = true;
            TryToSell();
        }
    }

    private void LateUpdate()
    {
        if (curSpeed < 0.1 && warmingUp == true)
        {
            if (Tourch.GetComponent<FroggerHotSpot>().HeatValue < 1)
            {
                Warmth(false, Tourch);
                return;
            }

            Vector3 targetFix = new Vector3(Tourch.transform.position.x, transform.position.y, Tourch.transform.position.z);
            Vector3 targetDir = targetFix - transform.position;

            float singleStep = turnSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, singleStep, 0.0f);

            Player.transform.rotation = Quaternion.LookRotation(newDir);

            if (Vector3.Angle(transform.forward,targetDir) < 3f)
            {
                theAnim.SetBool("Pray", true);
            }

        }
        else if (warmingUp == true)
        {
            theAnim.SetBool("Pray", false);
        }
       
        if(curSpeed < 0.1)
        {
            MoveIndicator.SetActive(false);
        }
    }

    public void TryToSell()
    {
        textObj.transform.rotation.SetLookRotation(textObj.transform.position - Camera.main.transform.position);
        theAnim.SetLayerWeight(1, 1f);
        theAnim.SetBool("Pray", true);
        counter -= Time.deltaTime;
        Vector3 lookAt = new Vector3(Person.transform.position.x, transform.position.y, Person.transform.position.z);
        Player.transform.LookAt(lookAt);

        if(counter < 0f)
        {
            textObj.text = "";
            theAnim.SetLayerWeight(1, prevWeight);
            theAnim.SetBool("Pray", false);
            selling = false;
        }
    }

    public void Warmth(bool heat,GameObject HeatSource)
    {
        Tourch = HeatSource;
        warmingUp = heat;

        if(heat == false)
        {
            theAnim.SetBool("Pray", heat);
        }
    }

    private void OnEnable()
    {
        inputLocation.Enable();
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputLocation.Disable();
        inputAction.Disable();
    }

}
