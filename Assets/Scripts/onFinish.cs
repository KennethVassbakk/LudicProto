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
    private GameObject EnemyRunner;

    private void Start() {
        _st = _gm.GetComponent<Endless_Story>();
        EnemyRunner = GameObject.FindGameObjectWithTag("Finish");
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

        } else if (other.gameObject.tag == "Env" &&  other.gameObject.layer == 10 == false) {
            
            Debug.Log("We've hit an obstruction!");
            this.gameObject.transform.parent.GetComponent<test_runner>().keepMoving = false;
            Failure.SetActive(true);
            Crossfade.GetComponent<Animator>().SetTrigger("Start");
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            _st.finish(false);
        }
    }

    private void FixedUpdate()
    {
        float LooseDistance = Vector3.Distance(transform.position, EnemyRunner.transform.position);

        if (LooseDistance > 20f)
        {
            this.gameObject.transform.parent.GetComponent<test_runner>().keepMoving = false;
            Failure.SetActive(true);
            Crossfade.GetComponent<Animator>().SetTrigger("Start");
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            _st.finish(false);
        }

    }
}
