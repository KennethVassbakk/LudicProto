/**
 *  Author: Kenneth Vassbakk 
 *  This is a really dumb runner AI
 *  It just runs forward, checks if i ts about to collider, and moves.
 *  It's biased towards the center lane.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunner : MonoBehaviour
{
    public float runSpeed = 7f;
    public Transform leftPos, centerPos, rightPos, ER;
    // X Movement
    private float targetPos = 0f;
    private Vector3 startPos;
    private float t = 0;
    public float MoveDelay = 1f;
    private float moveCount = 0f;
  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (moveCount <= 0f) {
            // CHeck if we are about to collide with something!
            if (!CheckClear(ER)) {

                // Move to center if it's clear
                // This also makes the enemy center biasec
                if (CheckClear(centerPos)) {
                    targetPos = centerPos.position.x;
                } else if (CheckClear(leftPos)) {
                    targetPos = leftPos.position.x;
                } else if (CheckClear(rightPos)) {
                    targetPos = rightPos.position.x;
                }
                startPos.x = ER.position.x;
                moveCount = MoveDelay;
            }
        } else if (moveCount > 0f) {

            // Move to X position
            t += Time.deltaTime / MoveDelay;
            ER.position = new Vector3(Mathf.Lerp(startPos.x, targetPos, t), ER.position.y, ER.position.z);

            if (t > 1.0f) {
                t = 0f;
                ER.position = new Vector3(targetPos, ER.position.y, ER.position.z);
            }

            moveCount -= Time.deltaTime;
        }


        // Move the player forward!
        transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);
    }

    public bool CheckClear(Transform t) {
        RaycastHit hit;
        if(Physics.Raycast(t.position, t.TransformDirection(Vector3.forward), out hit, 10f)) {
            Debug.DrawRay(t.position, t.TransformDirection(Vector3.forward) * hit.distance, Color.red); // Debug line!
            return false;
        } else {
            Debug.DrawRay(t.position, t.TransformDirection(Vector3.forward) * 10f, Color.yellow); // Debug line!
            return true;
        }
    }
}
