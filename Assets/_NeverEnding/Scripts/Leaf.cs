using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class Leaf : MonoBehaviour
{
    [Header("Components")]
    public Animator anmtr;
    public SplineFollower splineFollower;
    [Header("Types")]
    public LeafType leafType;
    public GameObject[] leafTypes;
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

    private void OnEnable()
    {
        anmtr.Play("ANM_Leaf_WindBlow", 1, Random.Range(0, 1.0f));
    }

    private void Start()
    {
        SetUpLeaf();
    }


    public void Grow()
    {
        isDone = true;
        SetLeafStage(1);
    }

    public void SetUpLeaf()
    {
        for(int i=0; i<leafTypes.Length;i++)
        {
            if(i == (int)leafType)
            {
                leafTypes[i].gameObject.SetActive(true);
            }
            else
            {
                leafTypes[i].gameObject.SetActive(false);
            }
        }
        SetLeafStage(0);
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
public enum LeafType
{
    Normal,
    Pine,
    Bill
}