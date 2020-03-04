using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endless_Story : MonoBehaviour
{

    public AudioClip startAudio;
    public AudioClip failAudio;
    public AudioClip sucessAudio;

    public GameObject player;
    public GameObject enemy;

    private AudioSource _as;

    private bool started = false;


    private void Start() {
        _as = GetComponent<AudioSource>();
        _as.clip = startAudio;

        _as.Play();
    }

    public void startGame() {
        Debug.Log("STARTING!");
        started = true;
        player.GetComponent<test_runner>().keepMoving = true;
        enemy.GetComponent<EnemyRunner>().keepMoving = true;
        _as.Stop();

    }
}
