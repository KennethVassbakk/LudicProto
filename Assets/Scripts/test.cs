using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    public GameObject testObj;
    public float timeOut = 10f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (timeOut <= 0) {
            createit();
            timeOut = 10f;
        }

        timeOut -= Time.deltaTime;
    }

    void createit() {
        PoolManager.Spawn(testObj, Vector3.zero, Quaternion.identity);
        Debug.Log("Created!");
    }
}
