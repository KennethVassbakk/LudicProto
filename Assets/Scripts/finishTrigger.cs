using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishTrigger : MonoBehaviour
{
    private GameObject _gm;
    private bool triggered = false;
    private void Start()
    {
        _gm = GameObject.Find("_GM");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        if (triggered) return;

        _gm.GetComponent<Story_Props>().FinishSuccess(true);
        triggered = true;
    }
}