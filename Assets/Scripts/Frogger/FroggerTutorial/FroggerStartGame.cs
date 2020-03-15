using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerStartGame : MonoBehaviour
{
    GameObject GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("_GM");
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<FroggerPlayer>().inControl = true;
        }
        if (other.CompareTag("TutorialPerson"))
        {
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            GM.GetComponent<FroggerLevelManager>().gameStarted = true;
            Destroy(transform.gameObject);
        }
    }
}
