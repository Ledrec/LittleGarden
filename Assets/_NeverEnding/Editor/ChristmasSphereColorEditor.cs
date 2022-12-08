using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChristmasSphereColor))]
public class ChristmasSphereColorEditor : Editor
{

    ChristmasSphereColor script;

    void Start()
    {
        script = (ChristmasSphereColor)target;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("RandomColor"))
        {
            script = (ChristmasSphereColor)target;
            Undo.RecordObject(script, "ChangeColor");
            script.RandomColor();
        }
        base.OnInspectorGUI();

    }
}
