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
        
        if(player)
            player.GetComponent<test_runner>().keepMoving = true;
        
        if(enemy)
            enemy.GetComponent<EnemyRunner>().keepMoving = true;
        
        StartCoroutine(FadeMixerGroup.StartFade(_as.outputAudioMixerGroup.audioMixer, "vol1", 1f, 0f, _as));
    }

    public void finish(bool success)
    {
        if (success)
            GameProperties.HaveShoes = 1;
        else
        {
            GameProperties.HaveShoes = 0;
        }

        if(success && failAudio) {
            _as.clip = sucessAudio;
            _as.Play();
        } else if (!success && failAudio) {
            _as.clip = failAudio;
            _as.Play();
        }
       
    }
}
