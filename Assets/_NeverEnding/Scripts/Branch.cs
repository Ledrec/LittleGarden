using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
public class Branch : MonoBehaviour
{
    [Header("Properties")]
    public bool isActive;
    public bool isDone;
    public double tipLength;
    public double startLength;
    public Tree tree;
    [Header("Movement Components")]
    public SplineComputer pathComputer;
    public SplineComputer rendererComputer;
    public List<SplineFollower> followingNodes;
    [Header("Speed")]
    [SerializeField] float minSpeed;
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
    public int activeLeaves;
    public List<Leaf> leaves;
    public List<Fruit> fruits;
    [Header("Sub Data")]
    public bool isSubBranch;
    public SplineFollower mainBranch;
    public float percentToStart;

    private void Start()
    {
        SetUp();
    }
    

    private void Update()
    {
        Grow();
    }

    void Grow()
    {
        if (isActive)
        {
            if (isDone)
            {
                if (tree.canGrowFruit)
                {
                    GrowFruit();
                }
            }
            else
            {
                CurrentMovement = Mathf.MoveTowards(currentMovement, Mathf.Max(GameManager.instance.targetMovement, isSubBranch ? 0 : minSpeed), Time.deltaTime * GameManager.instance.movementSpeedMultiplier) * branchSpeedMultiplier;
                if (followingNodes[0].GetPercent() >= 1)
                {
                    isDone = true;
                }
            }
        }
        //else
        //{
        //    if (isSubBranch)
        //    {
        //        if (mainBranch.GetPercent() >= percentToStart)
        //        {
        //            SetActive(true);
        //        }
        //    }
        //}
    }

    void SetUp()
    {
        CurrentMovement = 0;
        if (mainBranch != null)
        {
            isSubBranch = true;
            SetActive(false);
        }
        else
        {
            isActive = true;
        }
    }

    public void SetActive(bool _active)
    {
        isActive = _active;
        rendererComputer.gameObject.SetActive(_active);
    }

    void GrowFruit()
    {
        for(int i=0; i<fruits.Count; i++)
        {
            if(!fruits[i].fullyGrown)
            {
                fruits[i].timer+=Time.deltaTime;
            }
        }
    }
}
