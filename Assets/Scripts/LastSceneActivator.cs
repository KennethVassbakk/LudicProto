using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSceneActivator : MonoBehaviour
{
    public GameObject toActivate;
    public GameObject GM;
    public float timetospawn;
    private float count;

    public float endScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;

        if (timetospawn < count)
        {
            toActivate.SetActive(true);
        }

        if (endScene < count)
        {
            GM.GetComponent<SceneSwitcher>().NextLevel();
        }

    }
}
