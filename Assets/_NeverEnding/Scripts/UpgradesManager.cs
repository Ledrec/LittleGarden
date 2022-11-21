using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
   public void Upgrade(UpgradeType _type)
    {

    }
}
public enum UpgradeType
{
    AddBranch,
    AddLeaf,
    IncreaseSpeed
}