using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEndless : MonoBehaviour
{
    EndlessProperties _gm;
    // Start is called before the first frame update
    void Start()
    {
        _gm = GameObject.Find("_GM").GetComponent<EndlessProperties>();
    
        if(_gm.preGen > 0) {
            preGen();
        }
    }



    public void preGen() {
        for (int i = 0; i < _gm.preGen; i++) {
            Generate(new Vector3(0, 0, (int)(i * _gm.tileSize)));
        }
    }

    public void Generate(Vector3 position, bool pregen = false) {
        GameObject center = _gm.ground[Random.Range(0, _gm.ground.Length)];

        int location = (int)position.z + (_gm.tileSize * _gm.tileLength);
        PoolManager.Spawn(center, new Vector3(0, 0, pregen ? location : position.z), Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger entered!");
        if (other.gameObject.tag == "Env") {
            Generate(other.gameObject.transform.parent.gameObject.transform.position);
            PoolManager.Despawn(other.gameObject);
        }
    }
}
