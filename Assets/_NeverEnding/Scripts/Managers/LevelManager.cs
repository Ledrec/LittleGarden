using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelManager : MonoBehaviour
{
    public GameObject[] trees;
    public Transform treeParent;    
    public Tree activeTree;
    public System.Numerics.BigInteger sellPrice;

    private void Awake()
    {
        sellPrice = 1;
    }

    public void InstantiateTree(int _id)
    {
        GameObject go = Instantiate(trees[_id], Vector3.zero, Quaternion.identity, treeParent);
        activeTree = go.GetComponent<Tree>();
    }

    public void SellTree()
    {
        UIManager.instance.normalCurrencyCounter.ChangeCurrency(activeTree.GetSellPrice());
        Destroy(activeTree.gameObject);
        activeTree = null;
        InstantiateTree((int)Mathf.Repeat(SaveManager.LoadCurrentLevel(),trees.Length));
    }

    public void GoToNextLevel()
    {
        if(SaveManager.LoadCurrency(Currency.Regular)>=sellPrice)
        {
            UIManager.instance.fadeWindow.midAction = () => ChangeLevel();
            UIManager.instance.fadeWindow.Show();
        }
    }

    void ChangeLevel()
    {
        UIManager.instance.normalCurrencyCounter.ChangeCurrency(-sellPrice);
        SaveManager.SaveCurrentLevel(SaveManager.LoadCurrentLevel() + 1);
        SellTree();
    }
}
