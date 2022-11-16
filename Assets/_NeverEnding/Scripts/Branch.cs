using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
public class Branch : MonoBehaviour
{
    [Header("Properties")]
    public bool isActive;
    public double tipLength;
    public double startLength;
    [Header("Movement Components")]
    public SplineComputer pathComputer;
    public SplineComputer rendererComputer;
    public List<SplineFollower> followingNodes;
    [Header("Speed")]
    [SerializeField] float branchSpeedMultiplier;
    [SerializeField] float currentMovement;
    public float CurrentMovement
    {
        get 
        { 
            return currentMovement; 
        }
        set 
        { 
            currentMovement = value; 
            for(int i=0; i<followingNodes.Count; i++)
            {
                followingNodes[i].followSpeed = currentMovement;
            }
        }
    }
    [Header("Scaling")]
    public Vector2 scaleSetUpLimits;
    [Header("Sub Components")]
    public List<Branch> subBranches;
    public List<Leaf> leaves;
    [Header("Sub Data")]
    public bool isSubBranch;
    public SplineFollower mainBranch;
    public float percentToStart;
    private void Start()
    {
        CurrentMovement = 0;
        if(mainBranch != null)
        {
            isSubBranch = true;
            isActive = false;
            rendererComputer.gameObject.SetActive(false);
        }
        else
        {
            isActive = true;
        }
    }
    

    private void Update()
    {
        if(isActive)
        {
            if(!isSubBranch)
            {
                Debug.LogError(followingNodes[0].GetPercent());
            }
            CurrentMovement = Mathf.MoveTowards(currentMovement, GameManager.instance.targetMovement, Time.deltaTime * GameManager.instance.movementSpeedMultiplier)*branchSpeedMultiplier;
        }
        else
        {
            if (isSubBranch)
            {
                if (mainBranch.GetPercent() >= percentToStart)
                {
                    isActive = true;
                    rendererComputer.gameObject.SetActive(true);
                }
            }
        }
        
    }
}
