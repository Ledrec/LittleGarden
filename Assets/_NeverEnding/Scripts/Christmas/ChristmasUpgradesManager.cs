using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristmasUpgradesManager : UpgradesManager
{
    public override void AddBranch()
    {
        if (CanAddBranch())
        {
            for (int i = 0; i < ((ChristmasTree)GameManager.instance.levelManager.activeTree).christmasLights.Length; i++)
            {
                if (!((ChristmasTree)GameManager.instance.levelManager.activeTree).christmasLights[i].isDone && ((ChristmasTree)GameManager.instance.levelManager.activeTree).christmasLights[i].CanGrow)
                {
                    ((ChristmasTree)GameManager.instance.levelManager.activeTree).christmasLights[i].SetActive(true);
                    break;
                }
            }
        }
    }
    public override void AddFruit()
    {
        if(CanAddFruit())
        {
            for (int i = 0; i < ((ChristmasTree)GameManager.instance.levelManager.activeTree).christmasSpheres.Length; i++)
            {
                if (!((ChristmasTree)GameManager.instance.levelManager.activeTree).christmasSpheres[i].isDone && ((ChristmasTree)GameManager.instance.levelManager.activeTree).christmasSpheres[i].CanGrow)
                {
                    ((ChristmasTree)GameManager.instance.levelManager.activeTree).christmasSpheres[i].SetActive(true);
                    break;
                }
            }
        }
    }
    public override void AddLeaf()
    {
        if(CanAddLeaf())
        {
            for (int i = 0; i < ((ChristmasTree)GameManager.instance.levelManager.activeTree).leaves.Length; i++)
            {
                if (!((ChristmasTree)GameManager.instance.levelManager.activeTree).leaves[i].isDone && ((ChristmasTree)GameManager.instance.levelManager.activeTree).leaves[i].splineFollower.GetPercent() >= ((ChristmasTree)GameManager.instance.levelManager.activeTree).leaves[i].percentToAppear)
                {
                    ((ChristmasTree)GameManager.instance.levelManager.activeTree).leaves[i].Grow();
                    break;

                }
            }
        }
    }

   

    public override bool CanAddBranch()//Christmas Lights
    {
        for(int i=0; i< ((ChristmasTree)GameManager.instance.levelManager.activeTree).christmasLights.Length; i++)
        {
            if (!((ChristmasTree)GameManager.instance.levelManager.activeTree).christmasLights[i].isDone && ((ChristmasTree)GameManager.instance.levelManager.activeTree).christmasLights[i].CanGrow)
            {
                return true;
            }
        }
        return false;
    }

    public override bool CanAddFruit()//Christmas Spheres
    {
        for (int i = 0; i < ((ChristmasTree)GameManager.instance.levelManager.activeTree).christmasSpheres.Length; i++)
        {
            if (!((ChristmasTree)GameManager.instance.levelManager.activeTree).christmasSpheres[i].isDone && ((ChristmasTree)GameManager.instance.levelManager.activeTree).christmasSpheres[i].CanGrow)
            {
                return true;
            }
        }
        return false;
    }
    public override bool CanAddLeaf()
    {
        for (int i = 0; i < ((ChristmasTree)GameManager.instance.levelManager.activeTree).leaves.Length; i++)
        {
            if (!((ChristmasTree)GameManager.instance.levelManager.activeTree).leaves[i].isDone && ((ChristmasTree)GameManager.instance.levelManager.activeTree).leaves[i].splineFollower.GetPercent() >= ((ChristmasTree)GameManager.instance.levelManager.activeTree).leaves[i].percentToAppear)
            {
                return true;
            }
        }
        return false;
    }
}
