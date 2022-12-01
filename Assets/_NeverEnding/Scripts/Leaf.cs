using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class Leaf : MonoBehaviour
{
    public SplineFollower splineFollower;
    public Transform modelParent;
    [Header("Growth")]
    public bool isDone;
    public bool isActive;
    public float speedMultiplier = 1;
    
    public float currentMovement;
    public float CurrentMovement
    {
        get { return currentMovement; }
        set 
        { 
            currentMovement = value; 
            //modelParent.localScale += Vector3.one* 
        }
    }
    public float percentToAppear;
    public float minSpeed;
    public Vector2 growthRange;
    public float timer;

    private void Start()
    {
        modelParent.localScale = Vector3.Lerp(Vector3.one * growthRange.x, Vector3.one * growthRange.y, timer);
    }

    private void Update()
    {
        Grow();
    }

    public void Grow()
    {
        if (isActive)
        {
            if (!isDone)
            {
                CurrentMovement = Mathf.MoveTowards(currentMovement,Mathf.Max(minSpeed, GameManager.instance.targetMovement) * GameManager.instance.movementSpeedBonus, Time.deltaTime * GameManager.instance.movementSpeedMultiplier*speedMultiplier);
                timer += CurrentMovement;
                timer = Mathf.Clamp01(timer);
                modelParent.localScale = Vector3.Lerp(Vector3.one*growthRange.x, Vector3.one * growthRange.y,timer);
            }
        }

    }
}
