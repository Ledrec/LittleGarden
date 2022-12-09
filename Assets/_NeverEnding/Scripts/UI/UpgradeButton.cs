using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public bool canAfford;
    public bool canUpgrade;
    public bool maxLevel;
    public UpgradeType upgradeType;

    [Header("Components")]
    public Button button;
    public Animator anmtr;
    public TextMeshProUGUI txtCost;
    public TextMeshProUGUI txtUpgradeName;
    public Image background;
    public Sprite normalSprite;
    public Sprite christmasSprite;
    public CanvasGroup buttonGroup;

    public void SetState(float _cost, bool _canAfford, bool _canUpgrade, bool _maxLevel)
    {
        canUpgrade = _canUpgrade;
        canAfford = _canAfford;
        maxLevel = _maxLevel;
        SetState(_cost);
    }
    public void SetState(float _cost)
    {
        if (maxLevel)
        {
            //background.sprite = sprMaxLevel;
            txtCost.text = "MAX";
            buttonGroup.alpha = 1f;
            buttonGroup.interactable = false;
            button.interactable = false;

        }
        else if(canAfford && canUpgrade)
        {
            //background.sprite = sprNotMaxLevel;
            txtCost.text = "<sprite index=0>" + _cost.ToString();
            buttonGroup.alpha = 1f;
            buttonGroup.interactable = true;
            button.interactable = true;

            if(upgradeType == UpgradeType.AddBranch)
            {
                if(SaveManager.LoadOnlyTutorial() == 1)  //  Te alcanza para tu primera ramita
                {
                    UIManager.instance.CallSecondtTutorial();
                }
            }
        }
        else
        {
            //background.sprite = sprNotMaxLevel;
            txtCost.text = "<sprite index=0>" + _cost.ToString();
            buttonGroup.alpha = 0.5f;
            buttonGroup.interactable = false;
            button.interactable = false;
        }
    }
}
