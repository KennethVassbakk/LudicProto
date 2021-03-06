﻿/**
 *  Author: Kenneth Vassbakk 
 */
using UnityEngine;
/**
 *  Author: Kenneth Vassbakk 
 */
public class EndlessCameraFollow : MonoBehaviour
{
    public Transform target;
    public float smooth = 0.1f;
    public Vector3 cameraOffset;
    public float minX = -1f;
    public float maxX = 1f;
    public bool minMax = true;

    private void LateUpdate() {
        Vector3 desiredPos = target.position + cameraOffset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smooth);

        if (minMax)
        {
            // Set limits of position
            if (smoothPos.x <= minX) {
                smoothPos.x = minX;
            } else if (smoothPos.x >= maxX) {
                smoothPos.x = maxX;
            }

        }


        transform.position = smoothPos;
    }
}
