using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onFinish : MonoBehaviour
{
    public test_runner tr;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Finish") {
            Debug.Log("We've hit the target!");
        }
    }
}
