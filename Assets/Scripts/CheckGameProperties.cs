using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckGameProperties : MonoBehaviour
{
    public Text Shoes;
    public Text Scarf;

    public void Start()
    {
        CheckProps();
    }

    public void CheckProps()
    {
        Shoes.text = GameProperties.HaveShoes.ToString();
        Scarf.text = GameProperties.HaveScarf.ToString();
    }

    public void SetScarf()
    {
        GameProperties.HaveScarf = 1;
        CheckProps();
    }

    public void SetShoes()
    {
        GameProperties.HaveShoes = 1;
        CheckProps();
    }
}
