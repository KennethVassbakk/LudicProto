/**
 *  Author: John Hauge
 *  Script for updating our frost indicators
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostControl : MonoBehaviour
{
    //Amount of Frost ( 0 is warm, 100 is cold)
    [Range(0, 100)]public float Frost;

    private FrostEffect FrostFX;
    public Animator PlayerAnim;

    [Header("How often and how much, add / remove frost value")]
    public float frostUpdate;
    private float Timer;

    [Range(-50, 50)] public int FrostToGive;

    // Start is called before the first frame update
    void Start()
    {
        FrostFX = Camera.main.GetComponent<FrostEffect>();

        //SetTimer
        Timer = frostUpdate;
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if(Timer < 0f)
        {
            AddFrost(FrostToGive);
            Timer = frostUpdate;
        }
    }

    private void UpdateFrost()
    {
        FrostFX.FrostAmount = Frost / 100;
        PlayerAnim.SetLayerWeight(2, Frost / 100 - 0.1f);
    }

    public void AddFrost(int frostToAdd)
    {
        Frost += frostToAdd;
        UpdateFrost();
    }
}
