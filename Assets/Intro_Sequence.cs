using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_Sequence : MonoBehaviour
{
    public GameObject Hints;
    public void DisableAnimator()
    {
        this.GetComponent<Animator>().enabled = false;
        GiveHints();
    }

    public void GiveHints()
    {
        Hints.SetActive(true);
    }
}
