using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SCR_Light_Flicker : MonoBehaviour
{
    public bool flickIntensity;
    public float intensityRange = 0.2f;
    public float lerpDuration = 2f;

    private Light2D _light;
    private float _baseIntensity;

    private float oldIntensity;
    private float targetIntensity;
    private float timeElapsed;


    private void Start()
    {
        timeElapsed = 0f;

        _light = GetComponent<Light2D>();
        _baseIntensity = _light.intensity;

        setNewTargetIntensity();
    }

    private void FixedUpdate()
    {
        if (timeElapsed < lerpDuration)
        {
            timeElapsed += Time.deltaTime;
            _light.intensity = Mathf.Lerp(oldIntensity, targetIntensity, timeElapsed);
        }
        else
        {
            _light.intensity = targetIntensity;
            setNewTargetIntensity();
            timeElapsed = 0f;
        }
    }

    private void setNewTargetIntensity()
    {
        oldIntensity = _light.intensity;
        targetIntensity = Random.Range(_baseIntensity - intensityRange, _baseIntensity + intensityRange);
    }

}
