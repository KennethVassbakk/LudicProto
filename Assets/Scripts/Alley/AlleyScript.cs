using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleyScript : MonoBehaviour
{

    public Light TheLight;

    public FrostEffect Frost;
    public ParticleSystem PS;
    public GameObject button;
    public GameObject Stones;
    private Material _stones;

    public bool isLit = false;

    public float lightDuration = 5f;

    private float _currDur;

    private float _lightMaxIntensity;

    private float _lightCurrIntensity;
    private Color _currColor;

    public float _currFrost;
    public float FrostIntensity = 2f;
    public float FrostLightHelp = 2f;
    public float ShoesHelper = 0.5f;
    public float ScarfHelper = 0.5f;
    public float ColdThreshhold = 0.9f;

    [Header("Should be private but due to debug...")]
    public float ActualFrostIntensity = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _lightMaxIntensity = TheLight.intensity;
        _lightCurrIntensity = 0;
        TheLight.intensity = 0;
        _stones = Stones.GetComponent<Renderer>().material;
        _stones.SetColor("_Color", Color.white);

        ActualFrostIntensity = FrostIntensity / 100;
        ActualFrostIntensity = (GameProperties.HaveScarf == 1) ? ActualFrostIntensity - (ScarfHelper / 100) : ActualFrostIntensity;
        ActualFrostIntensity = (GameProperties.HaveShoes == 1) ? ActualFrostIntensity - (ShoesHelper / 100) : ActualFrostIntensity;

        _currFrost = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isLit)
        {
            if (_currDur < lightDuration)
                _currDur += Time.deltaTime;

            if (_currDur > lightDuration) {
                isLit = false;
                
                PS.Stop();
            }

            _currColor = _stones.GetColor("_Color");
            TheLight.intensity = Mathf.Lerp(TheLight.intensity, _lightMaxIntensity, Time.deltaTime / 1f);
            if (_currColor.a > 0)
            {
                _stones.SetColor("_Color", new Color(1,1,1, Mathf.Lerp(_currColor.a, 0, Time.deltaTime / 1f)));
            }

            _currFrost -= (FrostLightHelp /100) * Time.deltaTime;
        } else if (!isLit && TheLight.intensity > 0)
        {
            if (TheLight.intensity <= 0.1)
            {
                TheLight.intensity = 0f;
                button.SetActive(true);
                _stones.SetColor("_Color", new Color(1, 1, 1, 1));
            }
            else
            {
                _currColor = _stones.GetColor("_Color");
                TheLight.intensity = Mathf.Lerp(TheLight.intensity, 0, Time.deltaTime / 0.5f);
                _stones.SetColor("_Color", new Color(1, 1, 1, Mathf.Lerp(_currColor.a, 1, Time.deltaTime / 0.5f)));
            }
        }

        if(!isLit)
        {
            _currFrost += ActualFrostIntensity * Time.deltaTime;
        }

        Frost.FrostAmount = Mathf.Clamp(_currFrost, 0, 1);

        if (_currFrost > ColdThreshhold)
        {
            GetComponent<SceneSwitcher>().NextLevel();
            
        }
    }

    public void LightMatch()
    {
        if (isLit) return;

        isLit = true;
        _currDur = 0f;
        button.SetActive(false);
        PS.Play();
    }

}
