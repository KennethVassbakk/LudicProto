/**
 *  Author: Kenneth Vassbakk 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEndless : MonoBehaviour
{
    EndlessProperties _gm;
    
    void Start()
    {
        _gm = GameObject.Find("_GM").GetComponent<EndlessProperties>();
        preGen(_gm.tileLength);
    }

    /// <summary>
    /// Just itterates through Generate the amount of times needed to preload the map.
    /// </summary>
    /// <param name="tileLength"></param>
    public void preGen(int tileLength) {
        for (int i = 0; i < tileLength; i++) {
            Generate(new Vector3(0, 0, (int)(i * _gm.tileSize)), true);
        }
    }

    /// <summary>
    /// Generate environment ahead of the player.
    /// Pregen generates a load at the beginning. 
    /// </summary>
    /// <param name="position">Vector 3 position</param>
    /// <param name="pregen">True or false, default false.</param>
    public void Generate(Vector3 position, bool pregen = false) {
        GameObject center = _gm.ground[Random.Range(0, _gm.ground.Length)];

        int location = (int)position.z + (_gm.tileSize * _gm.tileLength);
        PoolManager.Spawn(center, new Vector3(0, 0, pregen ? position.z : location), Quaternion.identity);
    }

    /// <summary>
    /// When an environment item colliders with the trigger, it will despawn it and spawn a new set further ahead.
    /// saves memory.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Env") {
            Vector3 position = other.gameObject.transform.position;
            PoolManager.Despawn(other.gameObject);
            Generate(position);
        }
    }
}
