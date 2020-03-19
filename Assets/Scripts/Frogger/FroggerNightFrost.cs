using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerNightFrost : MonoBehaviour
{
    Rigidbody theRb;

    private bool haveLights;
    public GameObject Midlvl;

    private bool _arrived;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        theRb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (_arrived != false) return;

        theRb.MovePosition(transform.position + transform.right * Time.deltaTime);

        if (transform.position.x > Midlvl.transform.position.x || transform.position.z > Midlvl.transform.position.z)
            _arrived = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Tourch")) return;

        if (other.GetComponent<FroggerHotSpot>().playerInside == true)
        {
            other.GetComponent<FroggerHotSpot>().KillHeat();
        }

        other.tag = "Untagged";
        other.GetComponentInChildren<ParticleSystem>().Stop();
        other.GetComponentInChildren<FroggerHotSpot>().HeatValue = 0f;
        Light[] lights = other.GetComponentsInChildren<Light>();


        foreach (var i in lights)
        {
            i.gameObject.AddComponent<DimLights>();
        }


        if (other.CompareTag("Player"))
        {
            other.GetComponent<FroggerPlayer>().Dialogue("so ... cold ..", 2f);
        }
    }

}

/**
 * No this is not the best way to do it.
 * But i added it in here to fix an error
 * -- Kenneth
 */
public class DimLights : MonoBehaviour
{
    private Light _lightSource;

    private void Start() => _lightSource = GetComponent<Light>();

    private void Update()
    {
        _lightSource.intensity = Mathf.Lerp(_lightSource.intensity, 0f, Time.deltaTime / 2);

        if (_lightSource.intensity <= 0.1f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
