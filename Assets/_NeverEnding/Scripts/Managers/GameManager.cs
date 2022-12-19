using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using Tabtale.TTPlugins;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]public bool appearButtonTutorial;

    public LevelManager levelManager;
    public CameraTransition gameplayCameraTransition;
    [Header("Taps")]
    public float tapSpeedIncrease;
    public float tapSpeedDecrease;
    public Vector2 tapSpeedLimits;
    public float movementSpeedMultiplier;
    public float targetMovement;
    public float movementSpeedBonus;

    private void Awake()
    {

        TTPCore.Setup();

        //SaveManager.SaveCurrentLevel(1);
        instance = this;
        if (!PlayerPrefs.HasKey("LeafInvestment"))
        {
            PlayerPrefs.SetString("LeafInvestment", "0");
        }
        if (!PlayerPrefs.HasKey("BranchInvestment"))
        {
            PlayerPrefs.SetString("BranchInvestment", "0");
        }
        if (!PlayerPrefs.HasKey("FruitInvestment"))
        {
            PlayerPrefs.SetString("FruitInvestment", "0");
        }
    }

    private void Start()
    {
        appearButtonTutorial = false;
        levelManager.InstantiateTree((int)Mathf.Repeat(SaveManager.LoadCurrentLevel(), levelManager.trees.Length));

        if(SaveManager.LoadOnlyTutorial() == 0)  //  Viste el banner del tutorial
        {
            Invoke("CallTutorial", 1);
            SaveManager.ChangeOnlyTutorial(1);
        }
    }

    private void OnEnable()
    {
        LeanTouch.OnFingerDown += OnFingerDown;
    }
    private void OnDisable()
    {
        LeanTouch.OnFingerDown -= OnFingerDown;

    }

    public void CallTutorial()
    {
        UIManager.instance.CallFirstTutorial();
    }

    private void OnFingerDown(LeanFinger _finger)
    {
        targetMovement += tapSpeedIncrease;
        targetMovement = Mathf.Clamp(targetMovement, tapSpeedLimits.x, tapSpeedLimits.y);
        GameObject go =UIManager.instance.gameplayWindow.tapFeedbackPool.GetPooledObject();
        go.transform.position = _finger.GetWorldPosition(0.5f);
        if (SaveManager.LoadOnlyTutorial() == 1)  //  Viste el banner del tutorial
        {
            UIManager.instance.CloseFirstTutorial();
        }
    }
    private void Update()
    {
        targetMovement -= tapSpeedDecrease * Time.deltaTime;
        targetMovement = Mathf.Clamp(targetMovement, tapSpeedLimits.x, tapSpeedLimits.y);
    }
}
