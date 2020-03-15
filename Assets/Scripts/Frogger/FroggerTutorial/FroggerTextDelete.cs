using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FroggerTextDelete : MonoBehaviour
{
    public GameObject player;

    private float counter;
    private bool clicked;

    private Color tempColor;


    private void Start()
    {
        tempColor = GetComponent<TextMeshProUGUI>().color;
    }
    // Update is called once per frame
    void Update()
    {

        if (player.GetComponent<FroggerPlayer>().click == true)
        {
            clicked = true;
            tempColor = GetComponent<TextMeshProUGUI>().color;
        }
        if (clicked == true)
        {

            tempColor.a -= Time.deltaTime;
            GetComponent<TextMeshProUGUI>().color = tempColor;
            if (GetComponent<TextMeshProUGUI>().color.a < 0)
            {
                Destroy(transform.gameObject);
            }
        }

        counter += Time.deltaTime;

        if (counter > 3f && clicked == false)
        {
            GetComponent<TextMeshProUGUI>().text = "Click To Move";

            tempColor.a += Time.deltaTime;
            tempColor.a = Mathf.Clamp(tempColor.a, 0f, 39f);
            GetComponent<TextMeshProUGUI>().color = tempColor;


        }
    }
}
