using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class Tree : MonoBehaviour
{
    
    [Header("Fruit")]
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
   
}
