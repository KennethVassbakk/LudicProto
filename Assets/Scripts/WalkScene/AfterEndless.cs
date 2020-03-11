using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterEndless : MonoBehaviour
{

    private AudioSource _as;

    public AudioClip GotShoe;

    public AudioClip DontGotShoe;

    // Start is called before the first frame update
    void Start()
    {
        _as = GetComponent<AudioSource>();

        if (GameProperties.HaveShoes == 1 && GotShoe)
        {
            _as.clip = GotShoe;
            _as.Play();
        }

        if (GameProperties.HaveShoes == 0 && DontGotShoe)
        {
            _as.clip = DontGotShoe;
            _as.Play();
        }
    }


}
