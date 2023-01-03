using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Water_Shader : MonoBehaviour
{
    Renderer rend;
    public float voronoi_rate = 1f;
    private float offset;
        
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        offset += Time.deltaTime * voronoi_rate;
        rend.sharedMaterial.SetFloat("_Voronoi_Offset", offset);
    }
}
