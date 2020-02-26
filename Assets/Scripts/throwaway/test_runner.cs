using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_runner : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody _rb;
    public float Speed = 5f;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }
}
