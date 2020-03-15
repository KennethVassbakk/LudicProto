using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerTriggerDialogue : MonoBehaviour
{
    // Trigger Dialogue, Remember Rigidbody on the Object

    [TextArea(5, 10)]
    public string Dialogue;

    public float TextTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<FroggerPlayer>().Dialogue(Dialogue, TextTime);
        }
    }
}
