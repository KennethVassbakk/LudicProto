/**
 *  Author: John Hauge
 *  Script for updating our frost indicators
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostControlFrogger : MonoBehaviour
{
    //Amount of Frost ( 0 is warm, 100 is cold)
    [Range(0, 100)]public float Frost;
    private float frostLerp;

    private FrostEffect FrostFX;
    //private test_runner SpeedScript;
    private float storedSpeed;

    public Animator PlayerAnim;

    [Header("How often and how much, add / remove frost value")]
    public float frostUpdate;
    private float Timer;

    float smooth = 2f;
    float AnimWeightStore;
    

    [Range(-50, 50)] public float FrostToGive;

    // Start is called before the first frame update
    void Start()
    {
        FrostFX = Camera.main.GetComponent<FrostEffect>();
        //SpeedScript = GetComponent<test_runner>();
        //storedSpeed = SpeedScript.BaseSpeed;
        //SetTimer
        Timer = frostUpdate;
        AnimWeightStore = PlayerAnim.GetLayerWeight(2);
        AnimWeightStore *= 100f;

        
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if(Timer < 0f)
        {
            AddFrost(FrostToGive);
            Timer = frostUpdate;
            return;
        }

        //Frost = Mathf.Lerp(Frost, frostLerp, t);

        FrostFX.FrostAmount = Mathf.Clamp(Frost / 100, 0, 1);
        float FrostToAnim = Mathf.Clamp(Frost, AnimWeightStore, 100);
        
        PlayerAnim.SetLayerWeight(2, FrostToAnim / 100);
        Frost = Mathf.Lerp(Frost, frostLerp, Time.deltaTime * smooth);
    }

    private void UpdateFrost()
    {
        

        //SpeedScript.currentSpeed = Mathf.Clamp(storedSpeed - (Frost / 40), 0f, storedSpeed);
    }

    public void AddFrost(float frostToAdd)
    {
        frostLerp = Mathf.Clamp(Frost + frostToAdd, 0, 100);
    }
}
