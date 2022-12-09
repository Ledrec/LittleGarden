using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameplayWindow : Window
{
    [Header("Components")]
    public UpgradeButton[] upgradeButtons;
    public UpgradeButton[] christmasUpgradeButtons;
    public SellButton sellButton;
    public TextMeshProUGUI txtNextLevelPrice;
    private void Start()
    {
        SetUpUpgradeButtonsState();
        SetUpgradeButtonData();
    }
    private void Update()
    {
        SetUpUpgradeButtonsState();
        AutoToggleSellButton();
    }

    public void SetUpgradeButtonData()
    {
        for (int i = 0; i < UIManager.instance.gameplayWindow.upgradeButtons.Length; i++)
        {
            int x = i;
            UIManager.instance.gameplayWindow.GetUpgradeButton(upgradeButtons[i].upgradeType).button.onClick.RemoveAllListeners();
            UIManager.instance.gameplayWindow.GetUpgradeButton(upgradeButtons[i].upgradeType).button.onClick.AddListener(() => GameManager.instance.levelManager.activeTree.upgradesManager.Upgrade(upgradeButtons[x].upgradeType));
            //UIManager.instance.gameplayWindow.GetUpgradeButton(upgradeButtons[i].upgradeType).button.onClick.AddListener(() => UIManager.instance.gameplayWindow.GetUpgradeButton(upgradeButtons[x].upgradeType).anmtr.SetTrigger("Show"));

        }
    }
    public void AutoToggleSellButton()
    {
        if (GameManager.instance.levelManager.activeTree != null)
        {
            sellButton.gameObject.SetActive(GameManager.instance.levelManager.activeTree.mainBranch.GetGrowthPercent() >= GameManager.instance.levelManager.activeTree.percentToSell);
        }
        else
        {
            sellButton.gameObject.SetActive(false);
        }
    }
    public void SetUpUpgradeButtonsState()
    {
        Upgrade temp = GameManager.instance.levelManager.activeTree.upgradesManager.GetUpgradeType(UpgradeType.AddBranch);
        GetUpgradeButton(UpgradeType.AddBranch)?.SetState(
           Mathf.RoundToInt(temp.MultiplyLoop(temp.CurrentLevel, 0, temp.basePrice)),
           GameManager.instance.levelManager.activeTree.upgradesManager.CanBuy(UpgradeType.AddBranch),
           GameManager.instance.levelManager.activeTree.upgradesManager.CanAddBranch(),
           temp.CurrentLevel >= temp.maxLevel);

        temp = GameManager.instance.levelManager.activeTree.upgradesManager.GetUpgradeType(UpgradeType.AddLeaf);
        GetUpgradeButton(UpgradeType.AddLeaf)?.SetState(
          Mathf.RoundToInt(temp.MultiplyLoop(temp.CurrentLevel, 0, temp.basePrice)),
          GameManager.instance.levelManager.activeTree.upgradesManager.CanBuy(UpgradeType.AddLeaf),
          GameManager.instance.levelManager.activeTree.upgradesManager.CanAddLeaf(),
          temp.CurrentLevel >= temp.maxLevel);

        temp = GameManager.instance.levelManager.activeTree.upgradesManager.GetUpgradeType(UpgradeType.IncreaseSpeed);
        GetUpgradeButton(UpgradeType.IncreaseSpeed)?.SetState(
         Mathf.RoundToInt(temp.MultiplyLoop(temp.CurrentLevel, 0, temp.basePrice)),
         GameManager.instance.levelManager.activeTree.upgradesManager.CanBuy(UpgradeType.IncreaseSpeed),
         true,
         temp.CurrentLevel >= temp.maxLevel);

        temp = GameManager.instance.levelManager.activeTree.upgradesManager.GetUpgradeType(UpgradeType.AddFruit);
        GetUpgradeButton(UpgradeType.AddFruit)?.SetState(
         Mathf.RoundToInt(temp.MultiplyLoop(temp.CurrentLevel, 0, temp.basePrice)),
         GameManager.instance.levelManager.activeTree.upgradesManager.CanBuy(UpgradeType.AddFruit),
         GameManager.instance.levelManager.activeTree.upgradesManager.CanAddFruit(),
         temp.CurrentLevel >= temp.maxLevel);
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

    public void SetChristmasButtons(bool _isChristmas)
    {
        GetUpgradeButton(UpgradeType.AddBranch).txtUpgradeName.text = _isChristmas ? "ADD LIGHT" : "ADD BRANCH";
        GetUpgradeButton(UpgradeType.AddBranch).background.sprite = _isChristmas ? GetUpgradeButton(UpgradeType.AddBranch).christmasSprite : GetUpgradeButton(UpgradeType.AddBranch).normalSprite;
        GetUpgradeButton(UpgradeType.AddFruit).txtUpgradeName.text = _isChristmas ? "ADD SPHERE" : "ADD FRUIT";
        GetUpgradeButton(UpgradeType.AddFruit).background.sprite = _isChristmas ? GetUpgradeButton(UpgradeType.AddFruit).christmasSprite : GetUpgradeButton(UpgradeType.AddFruit).normalSprite;

    }
}
