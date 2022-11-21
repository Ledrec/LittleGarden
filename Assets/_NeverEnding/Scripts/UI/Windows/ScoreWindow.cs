using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreWindow : Window
{
    [Space(10)]
    public int reward;
    public float startDelay;
    [Header("Children")]
    public Button btnNext;
    [Header("Particles")]
    public ParticleMod moneyParticles;
    public GameObject confetti;

    #region Show/Hide
    public override void Show(bool _useCoroutine= true)
    {
        Setup();
        base.Show(_useCoroutine);
    }
    public override void Hide(bool _useCoroutine=true)
    {
        base.Hide(_useCoroutine);
        confetti.SetActive(false);
        //Subtegral.DialogueSystem.Runtime.DialogueParser.instance.SubscribeEasyTouch();
        UIManager.instance.normalCurrencyCounter.Hide();
    }
    public override IEnumerator IEShow()
    {
        //Fade
        confetti.SetActive(true);
        yield return new WaitForSeconds(startDelay);
        yield return StartCoroutine(base.IEShow());
    }

    #endregion

    void Setup()
    {
        confetti.SetActive(false);
        btnNext.interactable = true;
        UIManager.instance.normalCurrencyCounter.Show();
    }

    public void CompleteLevel()
    {
        btnNext.interactable = false;
        StartCoroutine(IECompleteLevel(2f));
    }
    
    IEnumerator IECompleteLevel(float _delay)
    {
        moneyParticles.MoveParticles();
        yield return new WaitForSeconds(_delay);
        UIManager.instance.normalCurrencyCounter.ChangeCurrency(reward,moneyParticles.startDelay+0.25f);
        yield return new WaitForSeconds(_delay);
        UIManager.instance.fadeWindow.midAction = () => LoadLevel();
        UIManager.instance.fadeWindow.Show();
        UIManager.instance.scoreWindow.Hide();
    }

    public void Retry()
    {
        UIManager.instance.fadeWindow.midAction = () => LoadLevel();
        UIManager.instance.scoreWindow.Hide();
        UIManager.instance.fadeWindow.Show();
    }

    private void LoadLevel()
    {

    }

}

