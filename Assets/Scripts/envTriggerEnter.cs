using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class envTriggerEnter : MonoBehaviour
{
    private GameObject _gm;
    private bool triggered = false;
    private void Start() {
        _gm = GameObject.Find("_GM");
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Env") return;
        if (triggered) return;

        _gm.GetComponent<Story_Props>().FinishSuccess(false);
        triggered = true;
    }
}
