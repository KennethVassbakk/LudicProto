/**
 *  Author: Kenneth Vassbakk 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onFinish : MonoBehaviour
{
    public test_runner tr;
    public GameObject Crossfade;
    public GameObject Success;
    public GameObject Failure;

    public GameObject _gm;
    private Endless_Story _st;

    private void Start() {
        _st = _gm.GetComponent<Endless_Story>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Finish") {
            Debug.Log("We've hit the target!");
            this.gameObject.transform.parent.GetComponent<test_runner>().keepMoving = false;
            other.gameObject.transform.parent.GetComponent<EnemyRunner>().keepMoving = false;
            Success.SetActive(true);
            Crossfade.GetComponent<Animator>().SetTrigger("Start");
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            _st.finish(true);

        } else if (other.gameObject.tag == "Env") {
            Debug.Log("We've hit an obstruction!");
            this.gameObject.transform.parent.GetComponent<test_runner>().keepMoving = false;
            Failure.SetActive(true);
            Crossfade.GetComponent<Animator>().SetTrigger("Start");
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            _st.finish(false);
        }
    }

}
