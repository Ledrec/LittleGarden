using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Editor;
using Dreamteck.Splines;
using UnityEditor;
[CustomEditor(typeof(Branch)), CanEditMultipleObjects]
public class BranchEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Set Up Rendering Nodes"))
        {
            SetUpBranchNodes();
        }
        if (GUILayout.Button("Link Nodes to Computer"))
        {
            SetNodesAsPoints();
        }
        if (GUILayout.Button("Remove Nodes to Computer"))
        {
            RemoveNodesAsPoints(); 
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
    public void SetNodesAsPoints()
    {
        Branch reference = (Branch)target;
        for(int i=0; i<reference.renderingNodes.Count; i++)
        {
            reference.renderingNodes[i].AddConnection(reference.rendererComputer, i);
        }
        for (int i = 0; i < reference.pathNodes.Count; i++)
        {
            reference.pathNodes[i].AddConnection(reference.pathComputer,i);
        }
    }
    public void RemoveNodesAsPoints()
    {
        Branch reference = (Branch)target;
        for (int i = 0; i < reference.renderingNodes.Count; i++)
        {
            reference.renderingNodes[i].RemoveConnection(reference.rendererComputer, i);
        }
        for (int i = 0; i < reference.pathNodes.Count; i++)
        {
            reference.pathNodes[i].RemoveConnection(reference.pathComputer, i);
        }
    }
}
