using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SellButton : MonoBehaviour
{
    public TextMeshProUGUI txtSellPrice;

    private void Update()
    {
        txtSellPrice.text = GameManager.instance.upgradesManager.currentTree.GetSellPrice().ToString();
    }
}
