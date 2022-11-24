using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyCounter : Window
{
    public Currency myCurrrency;
    public float stepsDuration = 1;
    [SerializeField]
    public TextMeshProUGUI txtCurrency;
    IEnumerator ieChange;

    private void Start()
    {
        ChangeTotalCurrency(SaveManager.LoadCurrency(myCurrrency));
    }

    #region RegularCurrency
    public void ChangeCurrency(BigInteger _currency, float _delay)
    {
        if (ieChange != null)
        {
            StopCoroutine(ieChange);
        }
        ieChange = IETextSteps(txtCurrency, SaveManager.LoadCurrency(myCurrrency), SaveManager.LoadCurrency(myCurrrency) + _currency, _delay);
        SaveManager.SaveCurrency(myCurrrency, _currency);
        StartCoroutine(ieChange);
    }
    public void ChangeTotalCurrency(BigInteger _totalCurrency, float _delay)
    {
        if (ieChange != null)
        {
            StopCoroutine(ieChange);
        }
        ieChange = IETextSteps(txtCurrency, SaveManager.LoadCurrency(myCurrrency), _totalCurrency, _delay);
        SaveManager.SaveTotalCurrency(myCurrrency, _totalCurrency);
        StartCoroutine(ieChange);
    }
    public void ChangeCurrency(BigInteger _currency)
    {
        if (ieChange != null)
        {
            StopCoroutine(ieChange);
        }
        ieChange = IETextSteps(txtCurrency, SaveManager.LoadCurrency(myCurrrency), SaveManager.LoadCurrency(myCurrrency) + _currency);
        SaveManager.SaveCurrency(myCurrrency, _currency);
        StartCoroutine(ieChange);
    }

    public void ChangeTotalCurrency(BigInteger _totalCurrency)
    {
        if (ieChange != null)
        {
            StopCoroutine(ieChange);
        }
        ieChange = IETextSteps(txtCurrency, SaveManager.LoadCurrency(myCurrrency), _totalCurrency);
        SaveManager.SaveTotalCurrency(myCurrrency, _totalCurrency);
        StartCoroutine(ieChange);
    }
    #endregion


    IEnumerator IETextSteps(TextMeshProUGUI _text, BigInteger _from, BigInteger _to, float _delay = 0.0f)
    {
        float timer = 0;
        BigInteger _diff = _to - _from;
        yield return new WaitForSecondsRealtime(_delay);
        while (timer < stepsDuration)
        {
            _text.text = (_from + (_diff * (int)(timer / stepsDuration * 100) / 100)).ToString();
            timer += Time.unscaledDeltaTime;
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        }
        _text.text = _to.ToString();
    }


}
