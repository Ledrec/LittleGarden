using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristmasLight : MonoBehaviour
{
    public Leaf leaf;
    public bool isActive;
    public bool isDone;
    public bool CanGrow
    {
        get
        {
            return leaf.isDone;
        }
    }
    public void SetActive(bool _isActive)
    {

    }
}
