/**
 *  Author: Kenneth Vassbakk 
 */
using UnityEngine;

public class EndlessCameraFollow : MonoBehaviour
{
    public Transform target;
    public float smooth = 5f;
    public Vector3 cameraOffset;

    private void Update() {
        Vector3 desiredPos = transform.position;
        if (target.position.x < -0.1f) {
            desiredPos = new Vector3(-1f, transform.position.y, transform.position.z);
        } else if (target.position.x > -0.1f & target.position.x < 0.1f) {
            desiredPos = new Vector3(0, transform.position.y, transform.position.z);
        } else if (target.position.x > 0.1f) {
            desiredPos = new Vector3(1f, transform.position.y, transform.position.z);
        }
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smooth * Time.deltaTime);

        transform.position = smoothPos;
    }
}
