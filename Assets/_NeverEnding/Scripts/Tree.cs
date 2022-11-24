using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class Tree : MonoBehaviour
{

    [Header("Fruit")]
    public Branch mainBranch;
    public Branch[] mainSubBranches;
    public Branch[] allBranches;
    public Branch[] allSubBranches;
    public bool canGrowFruit;
    int grownBranches;
    // Update is called once per frame
    void Update()
    {
        CheckIfCanGrowFruit();
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
   
    public int GetSellPrice()
    {
        int price = (int)(mainBranch.baseSellPrice*mainBranch.GetGrowthPercent());
        for (int i = 0; i < allSubBranches.Length; i++)
        {
            price += (int)(allSubBranches[i].baseSellPrice * allSubBranches[i].GetGrowthPercent()*mainBranch.GetGrowthPercent())+allBranches[i].GetActiveLeaves();
        }
        return price;
    }

    public int GetNumberOfBranches()
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
    public int GetNumberOfLeaves()
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
}
