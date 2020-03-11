using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveScarf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameProperties.HaveScarf != 1)
            this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
