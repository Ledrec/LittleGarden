using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("Windows")]
    public GameplayWindow gameplayWindow;
    public FadeWindow fadeWindow;
    public ScoreWindow scoreWindow;
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
}