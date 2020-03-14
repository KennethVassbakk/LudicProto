using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerAddFrost : MonoBehaviour
{
    public int FrostAmount;
    public int TimerInterval;

    private float timer;
    public FrostControlFrogger FrostControl;
    private bool playerHere;
    // Start is called before the first frame update
    void Start()
    {
        timer = TimerInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHere == true)
        {
            timer -= Time.deltaTime;

            if (timer < 0f)
            {
                FrostControl.AddFrost(FrostAmount);
                timer = TimerInterval;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHere = true;
            FrostControl.AddFrost(FrostAmount);
            Camera.main.GetComponent<affectFrost>().setFreezing(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) { 
            playerHere = false;
            Camera.main.GetComponent<affectFrost>().setFreezing(false);
        }
    }

}
