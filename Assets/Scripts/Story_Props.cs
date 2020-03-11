using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story_Props : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject CrossFade;
    public GameObject SuccessUI;
    public GameObject FailureUI;

    [Header("Units")]
    public GameObject Player;
    public GameObject Enemy;

    [Header("Audio")] 
    public AudioClip Intro;
    public AudioClip Success;
    public AudioClip Failure;

    private AudioSource _as;

    private void Start()
    {
        _as = GetComponent<AudioSource>();

        if (!Intro) return;

        _as.clip = Intro;
        _as.Play();
    }

    public void EndRun()
    {
        if (Player)
            Player.GetComponent<test_runner>().keepMoving = false;

        if (Enemy)
            Enemy.GetComponent<EnemyRunner>().keepMoving = false;
    }

    public void FinishSuccess(bool success) {
        EndRun();
        Debug.Log("Finished : " + success);
        if (Success && success)
        {
            _as.clip = Success;
            _as.Play();
        }


        if (Failure && !success)
        {
            _as.clip = Failure;
            _as.Play();
        }

        if (success)
        {
            if(SuccessUI)
                SuccessUI.SetActive(true);
            
        }
        else
        {
            if(FailureUI)
                FailureUI.SetActive(true);
        }

        CrossFade.GetComponent<Animator>().SetTrigger("Start");
    }

}
