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
    public Animator tapBranchScreen;
    public Animator tapSellScreen;
    public Animator secondHandTutorial;
    public Animator thirdHandTutorial;
    [Header("Currency")]
    public CurrencyCounter normalCurrencyCounter;
    public RewardParticles2D normalCurrencyParticles;

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
        touchScreen.gameObject.SetActive(true);
        touchScreen.SetBool(showAnimation, true);
    }

    public void CloseFirstTutorial()
    {
        touchScreen.SetBool(showAnimation, false);
        touchScreen.gameObject.SetActive(true);
    }

    public void CallSecondtTutorial()
    {
        tapBranchScreen.gameObject.SetActive(true);
        tapBranchScreen.SetBool(showAnimation, true);
        secondHandTutorial.gameObject.SetActive(true);
        secondHandTutorial.SetBool(showAnimation, true);
    }

    public void CloseSecondTutorial()
    {
        StartCoroutine(SecondTutorialTimer());
    }

    IEnumerator SecondTutorialTimer()
    {
        tapBranchScreen.SetBool(showAnimation, false);
        secondHandTutorial.SetBool(showAnimation, false);
        yield return new WaitForSeconds(0.5f);
        tapBranchScreen.gameObject.SetActive(false);
        secondHandTutorial.gameObject.SetActive(true);
    }

    public void CallThirdtTutorial()
    {
        tapSellScreen.gameObject.SetActive(true);
        tapSellScreen.SetBool(showAnimation, true);
        thirdHandTutorial.gameObject.SetActive(true);
        thirdHandTutorial.SetBool(showAnimation, true);
    }

    public void CloseThirdTutorial()
    {
        StartCoroutine(ThirdTutorialTimer());
    }

    IEnumerator ThirdTutorialTimer()
    {
        tapSellScreen.SetBool(showAnimation, false);
        thirdHandTutorial.SetBool(showAnimation, false);
        yield return new WaitForSeconds(0.5f);
        tapSellScreen.gameObject.SetActive(false);
        thirdHandTutorial.gameObject.SetActive(false);
    }
}