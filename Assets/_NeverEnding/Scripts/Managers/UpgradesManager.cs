using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public List<Upgrade> upgrades = new List<Upgrade>();
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
        for(int i=0; i<GameManager.instance.levelManager.activeTree.allSubBranches.Length; i++)
        {
            if(GameManager.instance.levelManager.activeTree.allSubBranches[i].parentBranch.GetGrowthPercent() >= GameManager.instance.levelManager.activeTree.allSubBranches[i].percentToStart && !GameManager.instance.levelManager.activeTree.allSubBranches[i].isActive && GameManager.instance.levelManager.activeTree.allSubBranches[i].isSubBranch)
            {
                return true;
            }
        }
        return false;
    }
    public bool CanAddLeaf()
    {
        for (int i = 0; i < GameManager.instance.levelManager.activeTree.allBranches.Length; i++)
        {
            for (int j = 0; j < GameManager.instance.levelManager.activeTree.allBranches[i].leaves.Count; j++)
            {
                if (GameManager.instance.levelManager.activeTree.allBranches[i].leaves[j].splineFollower.GetPercent() >= GameManager.instance.levelManager.activeTree.allBranches[i].leaves[j].percentToAppear && !GameManager.instance.levelManager.activeTree.allBranches[i].leaves[j].isDone)
                {
                    return true;
                }
            }
                
        }
        return false;
    }
    public bool CanAddFruit()
    {
        for (int i = 0; i < GameManager.instance.levelManager.activeTree.allBranches.Length; i++)
        {
            for (int j = 0; j < GameManager.instance.levelManager.activeTree.allBranches[i].fruits.Count; j++)
            {
                if (!GameManager.instance.levelManager.activeTree.allBranches[i].fruits[j].canGrow && GameManager.instance.levelManager.activeTree.allBranches[i].fruits[j].leafParent.isDone && !GameManager.instance.levelManager.activeTree.allBranches[i].fruits[j].isDone)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool CanAddLights()
    {
        for (int i = 0; i < GameManager.instance.levelManager.activeTree.allBranches.Length; i++)
        {
            for (int j = 0; j < GameManager.instance.levelManager.activeTree.allBranches[i].lights.Count; j++)
            {
                if (!GameManager.instance.levelManager.activeTree.allBranches[i].lights[j].canGrow && GameManager.instance.levelManager.activeTree.allBranches[i].lights[j].leafParent.isDone && !GameManager.instance.levelManager.activeTree.allBranches[i].lights[j].isDone)
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
            for (int i = 0; i < GameManager.instance.levelManager.activeTree.allSubBranches.Length; i++)
            {
                if (GameManager.instance.levelManager.activeTree.allSubBranches[i].parentBranch.GetGrowthPercent() >= GameManager.instance.levelManager.activeTree.allSubBranches[i].percentToStart && !GameManager.instance.levelManager.activeTree.allSubBranches[i].isActive && GameManager.instance.levelManager.activeTree.allSubBranches[i].isSubBranch)
                {
                    GameManager.instance.levelManager.activeTree.allSubBranches[i].SetActive(true);
                    break;
                }
            }
        }
    }

    public void AddLeaf()
    {
        bool done = false;
        for (int i = 0; i < GameManager.instance.levelManager.activeTree.allBranches.Length; i++)
        {
            for (int j = 0; j < GameManager.instance.levelManager.activeTree.allBranches[i].leaves.Count; j++)
            {
                if (GameManager.instance.levelManager.activeTree.allBranches[i].leaves[j].splineFollower.GetPercent() >= GameManager.instance.levelManager.activeTree.allBranches[i].leaves[j].percentToAppear && !GameManager.instance.levelManager.activeTree.allBranches[i].leaves[j].isActive)
                {
                    GameManager.instance.levelManager.activeTree.allBranches[i].leaves[j].isActive=true;
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
    public void AddFruit()
    {
        bool done = false;
        for (int i = 0; i < GameManager.instance.levelManager.activeTree.allBranches.Length; i++)
        {
            for (int j = 0; j < GameManager.instance.levelManager.activeTree.allBranches[i].fruits.Count; j++)
            {
                if (!GameManager.instance.levelManager.activeTree.allBranches[i].fruits[j].canGrow&& !GameManager.instance.levelManager.activeTree.allBranches[i].fruits[j].isDone)
                {
                    GameManager.instance.levelManager.activeTree.allBranches[i].fruits[j].canGrow=true;
                    done = true;
                    break;
                }
            }
            if (done)
            {
                break;
            }
        }
    }

    public void AddLights()
    {
        bool done = false;

        for (int i = 0; i < GameManager.instance.levelManager.activeTree.allBranches.Length; i++)
        {
            for (int j = 0; j < GameManager.instance.levelManager.activeTree.allBranches[i].lights.Count; j++)
            {
                if (!GameManager.instance.levelManager.activeTree.allBranches[i].lights[j].canGrow && GameManager.instance.levelManager.activeTree.allBranches[i].lights[j].leafParent.isDone && !GameManager.instance.levelManager.activeTree.allBranches[i].lights[j].isDone)
                {
                    GameManager.instance.levelManager.activeTree.allBranches[i].lights[j].canGrow = true;
                    done = true;
                    break;
                }

            }
            if (done)
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
    IncreaseSpeed,
    AddFruit,
    AddLights
}