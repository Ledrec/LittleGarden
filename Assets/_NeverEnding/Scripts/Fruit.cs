using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [Header("Scaling")]
    public Vector2 scaleLimits;
    public Transform scaleParent;
    public float timeToFullyGrow;
    public float timer;
    public float startGrowDelay;
    public bool fullyGrown;

    private void Update()
    {
        Grow();
    }

    void Grow()
    {
        if(!fullyGrown)
        {
            scaleParent.localScale = Vector3.one * Mathf.Lerp(scaleLimits.x, scaleLimits.y, Mathf.Clamp01((timer-startGrowDelay) / timeToFullyGrow));
            if (Mathf.Clamp01((timer - startGrowDelay) / timeToFullyGrow) >= 1)
            {
                fullyGrown = true;
            }
        }
    }
}
