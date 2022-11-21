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


    private void Start()
    {
        //ChangeTotalCurrency(SaveManager.LoadCurrency(myCurrrency));
    }

    #region RegularCurrency
    public void ChangeCurrency(int _currency, float _delay)
    {
        StartCoroutine(IETextSteps(txtCurrency, SaveManager.LoadCurrency(myCurrrency), SaveManager.LoadCurrency(myCurrrency) + _currency,_delay));
        SaveManager.SaveCurrency(myCurrrency, _currency);
    }
    public void ChangeTotalCurrency(int _totalCurrency, float _delay)
    {
        StartCoroutine(IETextSteps(txtCurrency, SaveManager.LoadCurrency(myCurrrency), _totalCurrency, _delay));
        SaveManager.SaveTotalCurrency(myCurrrency, _totalCurrency);
    }
    public void ChangeCurrency(int _currency)
    {
        StartCoroutine(IETextSteps(txtCurrency, SaveManager.LoadCurrency(myCurrrency),SaveManager.LoadCurrency(myCurrrency)+_currency));
        SaveManager.SaveCurrency(myCurrrency,_currency);
    }

    public void ChangeTotalCurrency(int _totalCurrency)
    {
        StartCoroutine(IETextSteps(txtCurrency, SaveManager.LoadCurrency(myCurrrency), _totalCurrency));
        SaveManager.SaveTotalCurrency(myCurrrency,_totalCurrency);
    }
    #endregion


    IEnumerator IETextSteps(TextMeshProUGUI _text, int _from, int _to)
    {
        float timer = 0;
        while(timer < stepsDuration)
        {
            _text.text = ((int)Mathf.Lerp(_from, _to, timer / stepsDuration)).ToString();
            timer += Time.deltaTime;
            yield return null;
        }
        _text.text = _to.ToString();
    }

    IEnumerator IETextSteps(TextMeshProUGUI _text, int _from, int _to, float _delay)
    {
        float timer = 0;
        while(timer < stepsDuration)
        {
            _text.text = ((int)Mathf.Lerp(_from, _to, timer / stepsDuration)).ToString();
            timer += Time.deltaTime;
            yield return null;
        }
        _text.text = _to.ToString();
    }


}
