using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(TilePerlinNoiseScript))]
public class TilePerlinNoiseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TilePerlinNoiseScript tilePerlinNoiseScript = (TilePerlinNoiseScript)target;

        if(GUILayout.Button("Generate Perlin Map"))
        {
            tilePerlinNoiseScript.GenerateTilemap();
        }
    }
}
