using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerPraying : MonoBehaviour
{
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnMouseDown()
    {
        Debug.Log("Hello");
        Vector3 Direction = Player.transform.position - transform.position;
        float dist = Direction.magnitude;

        if (dist < 2f)
        {
            Player.GetComponent<FroggerPlayer>().TryToSell();
        }
    }
}
