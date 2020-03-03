/**
 *  Author: Kenneth Vassbakk 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_runner : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody _rb;
    public float Speed = 5f;

    public Transform player;
    // Input Actions
    PlayerInput inputAction;

    // X Movement
    public float targetPos = 0f;
    private Vector3 startPos;
    private float t = 0;
    public float MoveDelay = 1f;
    public float moveCount = 0f;

    // Move
    Vector2 movementInput;

    private void Awake() {
        inputAction = new PlayerInput();
        inputAction.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        moveCount = MoveDelay;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        float h = movementInput.x;

        float currH = player.position.x;
        if (moveCount <= 0f && movementInput.magnitude > 0.1f) {
            if (h > 0.1f && targetPos < 1.5f && currH < 1.5f && CheckClear(player.position.x + 1.5f)) {
                targetPos += 1.5f;
                startPos = player.position;
                moveCount = MoveDelay;
            } else if(h < 0.1f && targetPos > -1.5f && currH > -1.5f && CheckClear(player.position.x - 1.5f)) {
                targetPos -= 1.5f;
                startPos = player.position;
                moveCount = MoveDelay;
            }
        } else if (moveCount > 0f) {

            // Move to X position
            t += Time.deltaTime / MoveDelay;
            player.position = new Vector3(Mathf.Lerp(startPos.x, targetPos, t), player.position.y, player.position.z);

            if (t > 1.0f) {
                t = 0f;
                player.position = new Vector3(targetPos, player.position.y, player.position.z);
            }

            moveCount -= Time.deltaTime;
        }


        // Move the player forward!
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }

    /// <summary>
    /// Checks if it's clear to the side the player wants to move
    /// </summary>
    /// <param name="posX"></param>
    /// <returns></returns>
    private bool CheckClear(float posX) {
        Collider[] hitColliders = Physics.OverlapSphere(new Vector3(posX, player.position.y, player.position.z), 0.5f);
        return (hitColliders.Length > 0) ? false : true;
    }

    private void OnEnable() {
        inputAction.Enable();
    }

    private void OnDisable() {
        inputAction.Disable();
    }
}
