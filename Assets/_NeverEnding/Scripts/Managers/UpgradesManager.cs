using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public List<Upgrade> upgrades = new List<Upgrade>();
    public IEnumerator upgradeFeedback;

    private void Start()
    {
        LoadUpgradeLevels();
    }

    public void LoadUpgradeLevels()
    {
        for(int i = 0; i < upgrades.Count; i++)
        {
            upgrades[i].LoadTotalUpgradeLevel();
        }
    }
    public void Upgrade(UpgradeType _type)
    {
        for(int i =0; i<upgrades.Count; i++)
        {
            if(upgrades[i].upgradeType == _type)
            {
                if(CanBuy(_type))
                {
                    if(upgrades[i].upgradeType == UpgradeType.AddBranch)  //  Vas a comprar tu primer ramita
                    {
                        if(SaveManager.LoadOnlyTutorial() == 1)  //  Compras tu primera ramita
                        {
                            SaveManager.ChangeOnlyTutorial(2);
                            UIManager.instance.CloseSecondTutorial();
                        }
                    }

                    UIManager.instance.normalCurrencyCounter.ChangeCurrency(-Mathf.RoundToInt(upgrades[i].MultiplyLoop(upgrades[i].totalUpgradeLevel, 0, upgrades[i].basePrice)));
                    upgrades[i].CurrentLevel++;
                    upgrades[i].totalUpgradeLevel++;
                    SaveManager.SaveTotalUpgradeLevel(upgrades[i].upgradeType, upgrades[i].totalUpgradeLevel);
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
                if (SaveManager.LoadCurrency(Currency.Regular) >= Mathf.RoundToInt(upgrades[i].MultiplyLoop(upgrades[i].totalUpgradeLevel, 0, upgrades[i].basePrice)))
                {
                    return true;
                }

            }
        }
        return false;
    }
    public virtual bool CanAddBranch()
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
    public virtual bool CanAddLeaf()
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
    public virtual bool CanAddFruit()
    {
        for (int i = 0; i < GameManager.instance.levelManager.activeTree.allBranches.Length; i++)
        {
            if(GameManager.instance.levelManager.activeTree.allBranches[i].GetGrowthPercent()>=1)
            {
                for (int j = 0; j < GameManager.instance.levelManager.activeTree.allBranches[i].fruits.Count; j++)
                {
                    if (!GameManager.instance.levelManager.activeTree.allBranches[i].fruits[j].canGrow && !GameManager.instance.levelManager.activeTree.allBranches[i].fruits[j].isDone)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public virtual void AddBranch()
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

    public virtual void AddLeaf()
    {
        bool done = false;
        for (int i = 0; i < GameManager.instance.levelManager.activeTree.allBranches.Length; i++)
        {
            for (int j = 0; j < GameManager.instance.levelManager.activeTree.allBranches[i].leaves.Count; j++)
            {
                if (GameManager.instance.levelManager.activeTree.allBranches[i].leaves[j].splineFollower.GetPercent() >= GameManager.instance.levelManager.activeTree.allBranches[i].leaves[j].percentToAppear && !GameManager.instance.levelManager.activeTree.allBranches[i].leaves[j].isDone)
                {
                    GameManager.instance.levelManager.activeTree.allBranches[i].leaves[j].Grow();
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
    public virtual void AddFruit()
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
    public void IncreaseSpeed()
    {
        GameManager.instance.movementSpeedBonus = 1f + (GetUpgradeType(UpgradeType.IncreaseSpeed).CurrentLevel*0.1f);
    }


}
[System.Serializable]
public class Upgrade
{
    public UpgradeType upgradeType;
    public int totalUpgradeLevel;
    [SerializeField] int currentLevel;
    public int CurrentLevel
    {
        get { return currentLevel; }
        set
        {
            currentLevel = value;
            if (GameManager.instance.levelManager.activeTree.upgradesManager.upgradeFeedback != null)
            {
                GameManager.instance.levelManager.activeTree.upgradesManager.StopCoroutine(GameManager.instance.levelManager.activeTree.upgradesManager.upgradeFeedback);
            }
            GameManager.instance.levelManager.activeTree.upgradesManager.upgradeFeedback = UpgradeFeedback();
            GameManager.instance.levelManager.activeTree.upgradesManager.StartCoroutine(GameManager.instance.levelManager.activeTree.upgradesManager.upgradeFeedback);
        }
    }
    public int basePrice;
    public float scaleDivider;
    public float scaleMultiplier;
    public int maxLevel;
    public UnityEngine.Events.UnityEvent levelUpEvent;

    IEnumerator UpgradeFeedback()
    {
        float eTime = 0;
        while (eTime < 1.0f)
        {
            eTime += Time.deltaTime;
            Shader.SetGlobalFloat("Offset", Mathf.Lerp(0.0f, 1.0f, eTime / 1.0f));
            yield return new WaitForEndOfFrame();
        }
    }

    public void LoadTotalUpgradeLevel()
    {
        totalUpgradeLevel = SaveManager.LoadTotalUpgradeLevel(upgradeType);
    }

    public float DivideLoop(int _totalIterations, int _currentIteration, float _currentValue)
    {
        if (_currentIteration < _totalIterations)
        {
            return DivideLoop(_totalIterations, _currentIteration + 1, _currentValue + (  _currentValue/scaleDivider));
        }
        return _currentValue;
    }
    public float MultiplyLoop(int _totalIterations, int _currentIteration, float _currentValue)
    {
        if (_currentIteration < _totalIterations)
        {
            return MultiplyLoop(_totalIterations, _currentIteration + 1, _currentValue*scaleMultiplier);
        }
        return _currentValue;
    }
}
public enum UpgradeType
{
    AddBranch,
    AddLeaf,
    IncreaseSpeed,
    AddFruit
}