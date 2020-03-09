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

    
    PlayerInput inputLocation;
    PlayerInput inputAction;

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
        Player = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ClickDetect > 0.1f)
        {
            Ray castPoint = Camera.main.ScreenPointToRay(Destination);

            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                Player.SetDestination(hit.point);
                
            }
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
