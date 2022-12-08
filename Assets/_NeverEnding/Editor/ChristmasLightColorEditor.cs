using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(ChristmasLightColor))]
public class ChristmasLightColorEditor : Editor
{

    ChristmasLightColor script;

    void Start()
    {
        script = (ChristmasLightColor)target;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("RandomColor"))
        {
            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                if (Selection.gameObjects[i].GetComponent<ChristmasLightColor>() != null)
                {
                    script = Selection.gameObjects[i].GetComponent<ChristmasLightColor>();
                    Undo.RecordObject(script, "ChangeColor");
                    script.RandomColor();
                }
            }
        }
        base.OnInspectorGUI();

    }
}
