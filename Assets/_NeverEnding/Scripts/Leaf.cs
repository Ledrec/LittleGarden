using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class Leaf : MonoBehaviour
{
    [Header("Components")]
    public Animator anmtr;
    public SplineFollower splineFollower;
    [Header("Growth")]
    public bool isDone;
    public float percentToAppear;
    public float percentToFullyGrow;
    [Header("Simulation")]
    public bool isSim;
    public float simTimer;
    public float simTimerSpeed;

    public void SetLeafStage(int _stage)
    {
        anmtr.SetInteger("Stage", _stage);
    }

    private void Start()
    {
        SetLeafStage(0);
    }


    public void Grow()
    {
        isDone = true;
        SetLeafStage(1);
    }


    #region Automated Growth
    void AutomateGrow()
    {
        if (isSim)
        {
            SimulateControlStage();
        }
        else
        {
            ControlStage();
        }
    }

    void ControlStage()
    {
        if(isDone)
        {
            return;
        }
        if (splineFollower.GetPercent() >= percentToAppear && anmtr.GetInteger("Stage")==0)
        {
            SetLeafStage(1);
        }
        else if (splineFollower.GetPercent() >= percentToFullyGrow && anmtr.GetInteger("Stage") == 1)
        {
            SetLeafStage(2);
            isDone = true;
        }
    }
    void SimulateControlStage()
    {
        if (isDone)
        {
            return;
        }
        simTimer += Time.deltaTime*simTimerSpeed;
        if (simTimer >= percentToAppear && anmtr.GetInteger("Stage") == 0)
        {
            SetLeafStage(1);
        }
        else if (simTimer >= percentToFullyGrow && anmtr.GetInteger("Stage") == 1)
        {
            SetLeafStage(2);
            isDone = true;
        }
    }
    #endregion
}
