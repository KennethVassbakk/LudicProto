/**
 *  Author: John Hauge
 *  Frogger Movement Script
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FroggerPlayer : MonoBehaviour
{
    private NavMeshAgent Player;
    private Vector3 Destination;
    private float ClickDetect;
    private float counter;

    PlayerInput inputLocation;
    PlayerInput inputAction;

    //Try to sell
    public float SaleTime;

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
        prevWeight = theAnim.GetLayerWeight(2);
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
                
                if (hit.transform.gameObject.CompareTag("Person"))
                {
                    Vector3 Direction = transform.position - hit.transform.position;

                    float dist = Direction.magnitude;
                    Debug.Log(dist);
                    if (dist < 4f)
                    {
                        Person = hit.transform.gameObject;
                        counter = SaleTime;
                        selling = true;
                        hit.transform.parent.GetComponent<FroggerPeople>().Interact(SaleTime);
                        return;
                    }
                }

                Player.SetDestination(hit.point);
            }
        }

        if (selling == true)
        {
            
            Player.isStopped = true;
            TryToSell();
        }
    }

    public void TryToSell()
    {
        
        theAnim.SetLayerWeight(2, 1f);
        theAnim.SetBool("Pray", true);
        counter -= Time.deltaTime;
        Vector3 lookAt = new Vector3(Person.transform.position.x, transform.position.y, Person.transform.position.z);
        Player.transform.LookAt(lookAt);

        if(counter < 0f)
        {
            theAnim.SetLayerWeight(2, prevWeight);
            theAnim.SetBool("Pray", false);
            selling = false;
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
