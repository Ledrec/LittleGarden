using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameplayWindow : Window
{
    [Header("Components")]
    public UpgradeButton[] upgradeButtons;
    public SellButton sellButton;
    private void Start()
    {
        SetUpUpgradeButtonsState();
        SetUpgradeButtonData();
    }
    private void Update()
    {
        SetUpUpgradeButtonsState();
    }

    public void SetUpgradeButtonData()
    {
        for (int i = 0; i < UIManager.instance.gameplayWindow.upgradeButtons.Length; i++)
        {
            int x = i;
            UIManager.instance.gameplayWindow.GetUpgradeButton(upgradeButtons[i].upgradeType).button.onClick.RemoveAllListeners();
            UIManager.instance.gameplayWindow.GetUpgradeButton(upgradeButtons[i].upgradeType).button.onClick.AddListener(() => GameManager.instance.upgradesManager.Upgrade(upgradeButtons[x].upgradeType));
            //UIManager.instance.gameplayWindow.GetUpgradeButton(upgradeButtons[i].upgradeType).button.onClick.AddListener(() => UIManager.instance.gameplayWindow.GetUpgradeButton(upgradeButtons[x].upgradeType).anmtr.SetTrigger("Show"));

        }
    }

    public void SetUpUpgradeButtonsState()
    {
        Upgrade temp = GameManager.instance.upgradesManager.GetUpgradeType(UpgradeType.AddBranch);
        GetUpgradeButton(UpgradeType.AddBranch)?.SetState(
           temp.price,
           GameManager.instance.upgradesManager.CanBuy(UpgradeType.AddBranch),
           GameManager.instance.upgradesManager.CanAddBranch(),
           temp.CurrentLevel > temp.maxLevel);

        temp = GameManager.instance.upgradesManager.GetUpgradeType(UpgradeType.AddLeaf);
        GetUpgradeButton(UpgradeType.AddLeaf)?.SetState(
          temp.price,
          GameManager.instance.upgradesManager.CanBuy(UpgradeType.AddLeaf),
          GameManager.instance.upgradesManager.CanAddLeaf(),
          temp.CurrentLevel > temp.maxLevel);

        temp = GameManager.instance.upgradesManager.GetUpgradeType(UpgradeType.IncreaseSpeed);
        GetUpgradeButton(UpgradeType.IncreaseSpeed)?.SetState(
         temp.price,
         GameManager.instance.upgradesManager.CanBuy(UpgradeType.IncreaseSpeed),
         true,
         temp.CurrentLevel > temp.maxLevel);

       
    }

    public UpgradeButton GetUpgradeButton(UpgradeType _upgradeType)
    {
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            if (upgradeButtons[i].upgradeType == _upgradeType)
            {
                return upgradeButtons[i];
            }
        }
        return null;
    }


}
