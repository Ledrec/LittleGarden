using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float tapSpeedIncrease;
    public float tapSpeedDecrease;
    public Vector2 tapSpeedLimits;
    public float movementSpeedMultiplier;
    public float targetMovement;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        LeanTouch.OnFingerDown += OnFingerDown;
    }
    private void OnDisable()
    {
        LeanTouch.OnFingerDown -= OnFingerDown;

    }

    private void OnFingerDown(LeanFinger _finger)
    {
        targetMovement += tapSpeedIncrease;
        targetMovement = Mathf.Clamp(targetMovement, tapSpeedLimits.x, tapSpeedLimits.y);
    }
    private void Update()
    {
        targetMovement -= tapSpeedDecrease * Time.deltaTime;
        targetMovement = Mathf.Clamp(targetMovement, tapSpeedLimits.x, tapSpeedLimits.y);

    }
}
