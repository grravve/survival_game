using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractMapGenerator), true)]

public class MapGeneratorEditor : Editor
{
    private AbstractMapGenerator _mapGenerator;

    private void Awake()
    {
        _mapGenerator = (AbstractMapGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Create map"))
        {
            _mapGenerator.GenerateMap();
        }
    }
}
