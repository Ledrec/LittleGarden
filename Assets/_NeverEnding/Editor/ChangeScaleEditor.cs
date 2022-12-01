using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChangeRandomScale))]
public class ChangeScaleEditor : Editor
{

	ChangeRandomScale script;
    // Start is called before the first frame update
    void Start()
    {
        script = (ChangeRandomScale)target;


    }

	public override void OnInspectorGUI()
	{
		if (GUILayout.Button("ChangeScale"))
		{
			script = (ChangeRandomScale)target;
			script.ChangeScale();
		}

		// Draw default inspector after button...
		base.OnInspectorGUI();
	}
}
