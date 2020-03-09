/**
 *  Author: Kenneth Vassbakk 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEndless : MonoBehaviour
{
    EndlessProperties _gm;

    // Used for triggering generation of new block.
    public GameObject genTrigger;
    int groundLength, leftLength, rightLength, leftObstrLength, rightObstrLength, cenObstrLength;
    int lastObstruction = 0;

    void Awake()
    {
        _gm = GameObject.Find("_GM").GetComponent<EndlessProperties>();

        // Store all the list lengths, this shouldn't change  during gameplay.
        groundLength = _gm.ground.Length;
        leftLength = _gm.leftSide.Length;
        rightLength = _gm.rightSide.Length;
        leftObstrLength = _gm.collidersLeft.Length;
        rightObstrLength = _gm.collidersRight.Length;
        cenObstrLength = _gm.collidersCenter.Length;

        preGen(_gm.tileLength);
    }

    /// <summary>
    /// Just itterates through Generate the amount of times needed to preload the map.
    /// </summary>
    /// <param name="tileLength"></param>
    public void preGen(int tileLength) {
        for (int i = 0; i < tileLength; i++) {
            Generate(new Vector3(0, 0, (int)(i * _gm.tileSize)), true, i < 3 ? false :  true); // Spawn sections, no traps the first three.
        }
    }

    /// <summary>
    /// Generate environment ahead of the player.
    /// Pregen generates a load at the beginning. 
    /// </summary>
    /// <param name="position"></param>
    /// <param name="pregen"></param>
    /// <param name="trap"></param>
    public void Generate(Vector3 position, bool pregen = false, bool trap = true) {
        // At what location is the items to be spawned?
        int location = (int)position.z + (_gm.tileSize * _gm.tileLength);

        GameObject ground = _gm.ground[Random.Range(0, groundLength)];
        GameObject leftSide = _gm.leftSide[Random.Range(0, leftLength)];
        GameObject rightSide = _gm.rightSide[Random.Range(0, rightLength)];

        if(trap) {
            // 0: Left 
            // 1: Left Center
            // 2: Center
            // 3: Center Right
            // 4 Right
            // 5: Left Right
            int trapPos = 0;
            while(trapPos == lastObstruction) {
                trapPos = Random.Range(0, 6);   // Where is the obstruction going to be? 0: left 1: center 2: right
            }
            lastObstruction = trapPos;

            float pos = pregen ? position.z : location;

            if (trapPos == 0 ) {
                placeObstruction(0, pos);

            } else if(trapPos == 1) {
                placeObstruction(0, pos);
                placeObstruction(1, pos);

            } else if(trapPos == 2) {
                placeObstruction(1, pos);

            } else if(trapPos == 3) {
                placeObstruction(1, pos);
                placeObstruction(2, pos);

            } else if(trapPos == 4) {
                placeObstruction(2, pos);

            } else if(trapPos == 5) {
                placeObstruction(1, pos);
                placeObstruction(2, pos);
            }

        }

        // Spawn Environment
        PoolManager.Spawn(ground, new Vector3(0, 0, pregen ? position.z : location), Quaternion.identity);      // Spawn Ground
        PoolManager.Spawn(leftSide, new Vector3(0, 0, pregen ? position.z : location), Quaternion.identity);    // Spawn Left Side
        PoolManager.Spawn(rightSide, new Vector3(0, 0, pregen ? position.z : location), Quaternion.identity);   // Spawn Right Side


        PoolManager.Spawn(genTrigger, new Vector3(0, 0, pregen ? position.z : location), Quaternion.identity); // Spawn the trigger for new generation
    }

    private void placeObstruction(int side, float location) {
        if(side == 0) {
            PoolManager.Spawn(_gm.collidersLeft[Random.Range(0, leftObstrLength)], new Vector3(-1.5f, 0f, location), Quaternion.identity);      // Spawn trap on the left side
        } else if(side == 1) {
            PoolManager.Spawn(_gm.collidersCenter[Random.Range(0, cenObstrLength)], new Vector3(0f, 0f, location), Quaternion.identity);        // Spawn trap in the center
        } else if(side == 2) {
            PoolManager.Spawn(_gm.collidersRight[Random.Range(0, rightObstrLength)], new Vector3(1.5f, 0f, location), Quaternion.identity);     // Spawn trap on the right side
        }
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
        }

        if (other.gameObject.tag == "GenTrigger") {
            // Time to generate!
            Vector3 position = other.gameObject.transform.position;
            Generate(position);
        }
    }
}
