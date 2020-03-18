using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterFrogger : MonoBehaviour
{

    private AudioSource _as;

    public AudioClip GotScarf;

    public AudioClip DontGotScarf;

    public float delay = 2f;
    private float _delay = 0f;
    private bool playing = false;

    // Start is called before the first frame update
    void Start()
    {
        _as = GetComponent<AudioSource>();


    }

    void Update()
    {
        _delay += Time.deltaTime;

        if (_delay >= delay && !playing) Play();
    }

    void Play()
    {
        playing = true;

        if (GameProperties.HaveShoes == 1 && GotScarf) {
            _as.clip = GotScarf;
            _as.Play();
        }

        if (GameProperties.HaveShoes == 0 && DontGotScarf) {
            _as.clip = DontGotScarf;
            _as.Play();
        }
    }

}
