using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustKeepWalking : MonoBehaviour
{

    public float WalkSpeed = 5f;


    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * WalkSpeed);
    }
}
