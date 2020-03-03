using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onFinish : MonoBehaviour
{
    public test_runner tr;
    public GameObject Crossfade;
    public GameObject Success;
    public GameObject Failure;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Finish") {
            Debug.Log("We've hit the target!");
            this.gameObject.transform.parent.GetComponent<test_runner>().keepMoving = false;
            other.gameObject.transform.parent.GetComponent<EnemyRunner>().keepMoving = false;
            Success.SetActive(true);
            Crossfade.GetComponent<Animator>().SetTrigger("Start");
            this.gameObject.GetComponent<BoxCollider>().enabled = false;

        } else if (other.gameObject.tag == "Env") {
            Debug.Log("We've hit an obstruction!");
            this.gameObject.transform.parent.GetComponent<test_runner>().keepMoving = false;
            Failure.SetActive(true);
            Crossfade.GetComponent<Animator>().SetTrigger("Start");
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

}
