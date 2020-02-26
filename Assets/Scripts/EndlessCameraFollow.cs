/**
 *  Author: Kenneth Vassbakk 
 */
using UnityEngine;

public class EndlessCameraFollow : MonoBehaviour
{
    public Transform target;
    public float smooth = 0.1f;
    public Vector3 cameraOffset;

    private void Update() {
        Vector3 desiredPos = target.position + cameraOffset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smooth);

        transform.position = desiredPos;
    }
}
