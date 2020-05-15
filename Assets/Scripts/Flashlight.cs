using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Flashlight : BaseObject
{
    private float _lighttime = 10;
    private float _timer;
    private Light _light;
    private KeyCode clic = KeyCode.F;
    private bool _isLighting;
    public GameObject[] batt;
    public GameObject lightSprite;
    public GameObject redBat;
    private int batCount = 5;
    public AudioClip buttonPress;

    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        _light = GetComponentInChildren<Light>();
        _timer = _lighttime;
    }

    private void FlashLightClic(bool val)
    {
        base.Audio.PlayOneShot(buttonPress);
        _light.enabled = val;
        lightSprite.SetActive(val);
       
    }
    // Update is called once per frame
    void Update()
    {
        int i = Mathf.RoundToInt(_timer / 2);
        if (i<1)
        {
            redBat.SetActive(true);
        }
        else
        {
            redBat.SetActive(false);
        }
        if (i< batCount && i>0)
        {
            batt[batCount-1].SetActive(false);
            batCount = i;
        }
        else if (i>batCount)
        {
            batt[batCount].SetActive(true);
            if (i > 0)
            {
                batCount = i;
            }
            else if (i<0)
            {
                batCount = 0;
            }
        }

       

        if (Input.GetKeyDown(clic))
        {
            _isLighting = !_isLighting;
            FlashLightClic(_isLighting);
        }
        if (_isLighting)
        {
            _timer -= Time.deltaTime;
            if (_timer <0)
            {
                FlashLightClic(false);
                _isLighting = false;
            }
        }
        else if (!_isLighting && _timer<_lighttime)
        {
            _timer += Time.deltaTime / 2;
        }
    }
}
