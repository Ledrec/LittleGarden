using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Branch))]
public class BranchEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Set Up Rendering Nodes"))
        {
            SetUpBranchNodes();
        }
    }

    public void SetUpBranchNodes()
    {
        Branch reference = (Branch)target;
        //Set Positions
        for (int i =0; i<reference.followingNodes.Count; i++)
        {
            if(i==0)//Tip
            {
                reference.followingNodes[i].SetClipRange(reference.tipLength+(reference.startLength / ((double)reference.followingNodes.Count) * (reference.followingNodes.Count-i - 1)), 1);
            }
            else if(i==1)
            {
                reference.followingNodes[i].SetClipRange(reference.tipLength/4+(reference.startLength / ((double)reference.followingNodes.Count) * (reference.followingNodes.Count - i - 1)), (1f - (1f / ((double)reference.followingNodes.Count)) * (i + 1)));

            }
            else
            {
                reference.followingNodes[i].SetClipRange((reference.startLength / ((double)reference.followingNodes.Count) * (reference.followingNodes.Count - i-1)), (1f - (1f /((float)reference.followingNodes.Count)) * (i+1)));
            }

        }
        //Set Sizes
        for(int i=0; i<reference.rendererComputer.pointCount; i++)
        {
            //Debug.LogError((float)i / (float)reference.rendererComputer.pointCount);
            reference.rendererComputer.SetPointSize(i, Mathf.Lerp(reference.scaleSetUpLimits.x, reference.scaleSetUpLimits.y, (float)i / (float)reference.rendererComputer.pointCount));
        }
    }
}
