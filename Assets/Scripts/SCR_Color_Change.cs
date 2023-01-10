using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SCR_Color_Change : MonoBehaviour
{
    public Color noPainColor;
    public Color fullPainColor;

    public float noPainTravelSpeed = 0.33f;
    public float fullPainTravelSpeed = 0.45f;

    public float noPainScale = 2.4f;
    public float fullPainScale = 3.0f;

    public float noPainRotationSpeed = 2.0f;
    public float fullPainRotationSpeed = 8.0f;


    private float lastPain;
    private float pain;

    private SCR_Scene_Manager.PlayerData playerData;
    private Light2D orbLight;
    private Material orbMaterial;


    private void Start()
    {
        orbLight = GetComponent<Light2D>();
        orbMaterial = GetComponent<SpriteRenderer>().material;
        playerData = SCR_Scene_Manager.instance.playerData;
        adjustPain();
    }
    void Update()
    {
        pain = SCR_Scene_Manager.instance.playerData.pain;
        if (pain != lastPain)
        {
            lastPain = pain;
            adjustPain();
        }
    }

    private void adjustPain()
    {
        float painPercent = SCR_Scene_Manager.instance.playerData.pain / SCR_Scene_Manager.instance.playerData.painMax;
        Color newColor = Color.Lerp(noPainColor, fullPainColor, painPercent);

        orbMaterial.SetFloat("_NoiseScale", Mathf.Lerp(noPainScale, fullPainScale, painPercent));
        orbMaterial.SetFloat("_NoiseRotationSpeed", Mathf.Lerp(noPainRotationSpeed, fullPainRotationSpeed, painPercent));
        orbMaterial.SetFloat("_NoiseTravelSpeed", Mathf.Lerp(noPainTravelSpeed, fullPainTravelSpeed, painPercent));
        orbMaterial.SetColor("_Color", newColor);
        orbLight.color = newColor;
        
    }
}
