using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAfter : MonoBehaviour
{
    public float Duration = 10f;

    private float _dur = 0f;
    private bool _ending = false;

    void Update()
    {
        if (_dur >= Duration && _ending == false)
        {
            GetComponent<SceneSwitcher>().NextLevel();
            _ending = true;
        }
        _dur += Time.deltaTime;
    }

}
