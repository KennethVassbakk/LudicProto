using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckShoe : MonoBehaviour
{
    void Start() {
        if (GameProperties.HaveShoes == 0)
            this.gameObject.SetActive(false);
    }
}
