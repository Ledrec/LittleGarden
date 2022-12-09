using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    readonly int showAnimation = Animator.StringToHash("isShown");

    public static UIManager instance;
    [Header("Windows")]
    public GameplayWindow gameplayWindow;
    public FadeWindow fadeWindow;
    public ScoreWindow scoreWindow;
    public Animator touchScreen;
    [Header("Currency")]
    public CurrencyCounter normalCurrencyCounter;
    public ParticleMod normalCurrencyParticles;

    /// <summary>
    /// Initialize values
    /// </summary>
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        gameplayWindow.Show();
        scoreWindow.Hide();
        normalCurrencyCounter.Show();
    }

    public void CallFirstTutorial()
    {
        touchScreen.SetBool(showAnimation, true);
    }

    public void CloseFirstTutorial()
    {
        touchScreen.SetBool(showAnimation, false);
    }
}