using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractMapGenerator), true)]

public class MapGeneratorEditor : Editor
{
    AbstractMapGenerator mapGenerator;

    private void Awake()
    {
        mapGenerator = (AbstractMapGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Create map"))
        {
            mapGenerator.GenerateMap();
        }
    }
}
