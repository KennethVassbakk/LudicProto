﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { GetComponentInParent<SceneSwitcher>().NextLevel(); }
    }
}
