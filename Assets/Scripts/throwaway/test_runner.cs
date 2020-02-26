using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_runner : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody _rb;
    public float Speed = 5f;
    public float MoveDelay = .2f;
    public float moveCount = 0f;
    public Transform player;
    // Input Actions
    PlayerInput inputAction;

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
            if (h > 0.1f && currH < 1.5f) {
                player.position = new Vector3(player.position.x + 1.5f, player.position.y, player.position.z);
            } else if (h < -0.1f && currH > -1.5f) {
                player.position = new Vector3(player.position.x - 1.5f, player.position.y, player.position.z);
            }

            moveCount = MoveDelay;
        } else if (moveCount > 0f) {
            moveCount -= Time.deltaTime;
        }

        // Move the player forward!
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }

    private void OnEnable() {
        inputAction.Enable();
    }

    private void OnDisable() {
        inputAction.Disable();
    }
}
