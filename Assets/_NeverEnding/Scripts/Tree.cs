using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Dreamteck.Splines;

public class Tree : MonoBehaviour
{

    [Header("Fruit")]
    public Branch mainBranch;
    public Branch[] mainSubBranches;
    public Branch[] allBranches;
    public Branch[] allSubBranches;
    public UpgradesManager upgradesManager;
    public bool canGrowFruit;
    public float percentToSell;
    int grownBranches;
    public bool isChristmasPine;

    public float editorGrow;

    void Update()
    {
        GameManager.instance.gameplayCameraTransition.SetPosition((float)mainBranch.GetGrowthPercent());
    }

    public void CheckIfCanGrowFruit()
    {
        if(!canGrowFruit)
        {
            grownBranches = 0;
            for (int i = 0; i < mainSubBranches.Length; i++)
            {
                if (mainSubBranches[i].isDone)
                {
                    grownBranches++;
                }
            }
            if (grownBranches >= 3)
            {
                canGrowFruit = true;
            }
        }
    }

    public virtual BigInteger GetSellPrice()
    {
        BigInteger price = (BigInteger)(mainBranch.baseSellPrice * SaveManager.LoadSoldTrees()  * mainBranch.GetGrowthPercent());
        BigInteger leafInvestment = SaveManager.GetInvestmentValue("Leaf");
        BigInteger branchInvestment = SaveManager.GetInvestmentValue("Branch");
        BigInteger fruitInvestment = SaveManager.GetInvestmentValue("Fruit");
        price += (leafInvestment + branchInvestment + fruitInvestment) * 15 / 10;
        return price;
    }

    public virtual int GetNumberOfBranches()
    {
        int temp = 0;
        for (int i = 0; i < allBranches.Length; i++)
        {
            if(allBranches[i].isActive)
            {
                temp ++;
            }
        }
        return temp;
    }
    public virtual int GetNumberOfLeaves()
    {
        int temp = 0;
        for (int i = 0; i < allBranches.Length; i++)
        {
            for (int j = 0; j < allBranches[i].leaves.Count; j++)
            {
                if (allBranches[i].leaves[j].isDone)
                {
                    temp++;
                }
            }
        }
        return temp;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        for (int i = 0; i < allBranches.Length; i++)
        {
            for (int k = 0; k < allBranches[i].rendererComputer.pointCount; k++)
            {
                SplinePoint point = allBranches[i].rendererComputer.GetPoint(k);
                point.size = 0.3f * editorGrow;
                allBranches[i].rendererComputer.SetPoint(k, point);
            }
            for (int j = 0; j < allBranches[i].followingNodes.Count; j++)
            {
                allBranches[i].followingNodes[j].Restart(editorGrow);
            }
        }
    }
#endif

}
