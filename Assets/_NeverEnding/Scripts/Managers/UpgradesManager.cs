using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public List<Upgrade> upgrades = new List<Upgrade>();
    public Tree currentTree;
   public void Upgrade(UpgradeType _type)
   {
        for(int i =0; i<upgrades.Count; i++)
        {
            if(upgrades[i].upgradeType == _type)
            {
                if(CanBuy(_type))
                {
                    UIManager.instance.normalCurrencyCounter.ChangeCurrency(-upgrades[i].price);
                    upgrades[i].CurrentLevel++;
                    upgrades[i].levelUpEvent.Invoke();
                }
            }
        }
   }

    public Upgrade GetUpgradeType(UpgradeType _type)
    {
        for (int i = 0; i < upgrades.Count; i++)
        {
            if (upgrades[i].upgradeType == _type)
            {
                return upgrades[i];
            }
        }
        return null;
    }

    public bool CanBuy(UpgradeType _type)
    {
        for (int i = 0; i < upgrades.Count; i++)
        {
            if (upgrades[i].upgradeType == _type)
            {
                if (SaveManager.LoadCurrency(Currency.Regular) >= upgrades[i].price)
                {
                    return true;
                }

            }
        }
        return false;
    }
    public bool CanAddBranch()
    {
        for(int i=0; i<currentTree.allSubBranches.Length; i++)
        {
            if(currentTree.allSubBranches[i].parentBranch.GetGrowthPercent() >= currentTree.allSubBranches[i].percentToStart && !currentTree.allSubBranches[i].isActive && currentTree.allSubBranches[i].isSubBranch)
            {
                return true;
            }
        }
        return false;
    }
    public bool CanAddLeaf()
    {
        for (int i = 0; i < currentTree.allBranches.Length; i++)
        {
            for (int j = 0; j < currentTree.allBranches[i].leaves.Count; j++)
            {
                if (currentTree.allBranches[i].leaves[j].splineFollower.GetPercent() >= currentTree.allBranches[i].leaves[j].percentToAppear && !currentTree.allBranches[i].leaves[j].isDone)
                {
                    return true;
                }
            }
                
        }
        return false;
    }


    public void AddBranch()
    {
        if(CanAddBranch())
        {
            for (int i = 0; i < currentTree.allSubBranches.Length; i++)
            {
                if (currentTree.allSubBranches[i].parentBranch.GetGrowthPercent() >= currentTree.allSubBranches[i].percentToStart && !currentTree.allSubBranches[i].isActive && currentTree.allSubBranches[i].isSubBranch)
                {
                    currentTree.allSubBranches[i].SetActive(true);
                    break;
                }
            }
        }
    }

    public void AddLeaf()
    {
        bool done = false;
        for (int i = 0; i < currentTree.allBranches.Length; i++)
        {
            for (int j = 0; j < currentTree.allBranches[i].leaves.Count; j++)
            {
                if (currentTree.allBranches[i].leaves[j].splineFollower.GetPercent() >= currentTree.allBranches[i].leaves[j].percentToAppear && !currentTree.allBranches[i].leaves[j].isDone)
                {
                    currentTree.allBranches[i].leaves[j].Grow();
                    done= true;
                    break;
                }
            }
            if(done)
            {
                break;
            }
        }
    }

    public void IncreaseSpeed()
    {
        GameManager.instance.movementSpeedBonus = 1f + (GetUpgradeType(UpgradeType.IncreaseSpeed).CurrentLevel*0.1f);
    }


}
[System.Serializable]
public class Upgrade
{
    public UpgradeType upgradeType;
    [SerializeField] int currentLevel;
    public int CurrentLevel
    {
        get { return currentLevel; }
        set { currentLevel = value; }
    }
    public int price;
    public int maxLevel;
    public UnityEngine.Events.UnityEvent levelUpEvent;

}
public enum UpgradeType
{
    AddBranch,
    AddLeaf,
    IncreaseSpeed
}