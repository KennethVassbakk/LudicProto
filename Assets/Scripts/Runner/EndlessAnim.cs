/**
 *  Author: John Hauge
 *  
 *  Based on test_runner.cs by Kenneth Vassbakk 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessAnim : MonoBehaviour
{
    private Animator theAnim;
    Vector2 movementInput;

    PlayerInput inputAction;

    private void Awake()
    {
        inputAction = new PlayerInput();
        inputAction.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
    }

    // Start is called before the first frame update
    void Start()
    {
        theAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movementInput.x < 0f)
        {
            theAnim.SetBool("StrafeLeft", true);
        }
        else if (movementInput.x > 0f)
        {
            theAnim.SetBool("StrafeRight", true);
        }
        else
        {
            theAnim.SetBool("StrafeLeft", false);
            theAnim.SetBool("StrafeRight", false);
        }
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }
}
