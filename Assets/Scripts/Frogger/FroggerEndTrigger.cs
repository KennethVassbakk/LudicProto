using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerEndTrigger : MonoBehaviour
{

    private GameObject GM;

    private void Awake()
    {
        GM = GameObject.Find("_GM");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { GM.GetComponent<SceneSwitcher>().NextLevel(); }
    }
}
