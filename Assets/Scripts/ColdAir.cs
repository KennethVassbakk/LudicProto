using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdAir : MonoBehaviour
{
    public List<GameObject> colliders;
    public float Speed;

    private int colCounter = 0;

    float positionTracker;

    private bool moveLeft;
    private bool moveRight;

    float newPos;

    // Start is called before the first frame update
    void Start()
    {
        positionTracker = transform.position.z - 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * Speed;

        if (transform.position.z < positionTracker)
        {
            Vector3 movePoint = new Vector3(transform.position.x, 1, transform.position.z);
            colliders[colCounter].transform.position = movePoint;
            colCounter += 1;
            if (colCounter == 3) { colCounter = 0; }
            positionTracker = transform.position.z - 2;

            RaycastHit hit;

            int layerMask = 1 << 10;
            layerMask = ~layerMask;

            if (Physics.Raycast(movePoint, transform.forward, out hit, 6f, layerMask) && moveLeft == false && moveRight == false)
            {
                if (moveLeft == false && moveRight == false && hit.transform.gameObject.CompareTag("Env"))
                {
                    if (transform.position.x < -0.5f)
                    {
                        newPos = transform.position.x + 2f;
                        moveLeft = true;
                        return;
                    }
                    if (transform.position.x > 0.5f)
                    {
                        newPos = transform.position.x - 2f;
                        moveRight = true;
                        return;
                    }
                }
                Vector3 nabourObstacleDir = hit.transform.position + new Vector3(0f,1f,0f);

                RaycastHit hit2;

                //Debug.DrawLine(movePoint, new Vector3(hit.transform.position.x - 4f, 1f, hit.transform.position.z));
                Debug.DrawRay(nabourObstacleDir, Vector3.left, Color.red, 2f);



                if (Physics.Raycast(nabourObstacleDir, Vector3.left, out hit2, 2f, layerMask))
                {
                    if (hit.transform.gameObject.CompareTag("Env") )
                    {
                        newPos = transform.position.x + 2f;
                        moveLeft = true;
                    }
                }
                if (moveLeft == false )
                {
                    moveRight = true;
                    newPos = transform.position.x - 2f;
                }
                else
                {
                    newPos = transform.position.x + 2f;
                    moveLeft = true;
                }
            }
        }
        if (moveLeft == true)
        {
            MoveLeft();
        }
        if (moveRight == true)
        {
            MoveRight();
        }
    }

    private void MoveLeft()
    {
        transform.position += Vector3.right * Time.deltaTime * Speed;

        float newpos = transform.position.x + 1.5f;

        if (transform.position.x > newPos)
        {
            moveLeft = false;
        }

        if (transform.position.x > 2f)
        {
            moveLeft = false;
        }
    }
    private void MoveRight()
    {
        transform.position += Vector3.left * Time.deltaTime * Speed;

        if (transform.position.x < newPos)
        {
            moveRight = false;
        }

        if (transform.position.x < -2f)
        {
            moveRight = false;
        }
    }
}
