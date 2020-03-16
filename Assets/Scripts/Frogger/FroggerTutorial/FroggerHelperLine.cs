using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerHelperLine : MonoBehaviour
{
    public GameObject player;
    public GameObject goal;

    private LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        line.SetPosition(1, player.transform.position + new Vector3(0f, 2f, 0f));
        line.SetPosition(0, goal.transform.position);
    }
}
