using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Currency
{
    Regular,
    Premium,
    Level
}
public class SaveManager
{
    #region Tutorials
    /// <summary>
    /// Sets _tutorial status based on completion
    /// </summary>
    /// <param name="_tutorial">Name of tutorial</param>
    /// <param name="_isDone">true if tutorial has been completed</param>
    public static void SaveTutorialState(string _tutorial, bool _isDone)
    {
        if(_isDone)
        {
            PlayerPrefs.SetInt(_tutorial, 3);
        }
        else
        {
            PlayerPrefs.SetInt(_tutorial, 0);
        }
    }

    public static void StartOnlyTutorial()
    {
        if(!PlayerPrefs.HasKey("OnlyTutorial"))
        {
            PlayerPrefs.SetInt("OnlyTutorial", 0);
        }
    }

    public static void ChangeOnlyTutorial(int _value)
    {
        PlayerPrefs.SetInt("OnlyTutorial", _value);
    }

    public static int LoadOnlyTutorial()
    {
        return PlayerPrefs.GetInt("OnlyTutorial");
    }

    /// <summary>
    /// Function returns true if the _tutorial has been completed
    /// </summary>
    /// <param name="_tutorial">Name of tutorial</param>
    /// <returns></returns>
    public static bool IsTutorialDone(string _tutorial)
    {
        return PlayerPrefs.GetInt(_tutorial, 0) == 3 ? true : false;
    }
    #endregion

    #region Regular Currency
    public static void SaveTotalRegularCurrency(int _totalCurrency)
    {
        PlayerPrefs.SetInt("RegularCurrency", _totalCurrency);
    }
    public static void SaveRegularCurrency(int _currency)
    {
        PlayerPrefs.SetInt("RegularCurrency", LoadRegularCurrency() + _currency);
    }
    public static int LoadRegularCurrency()
    {
        return PlayerPrefs.GetInt("RegularCurrency");
    }
    #endregion

    #region Premium Currency
    public static void SaveTotalPremiumCurrency(int _totalCurrency)
    {
        PlayerPrefs.SetInt("PremiumCurrency", _totalCurrency);
    }
    public static void SavePremiumCurrency(int _currency)
    {
        PlayerPrefs.SetInt("PremiumCurrency", LoadPremiumCurrency() + _currency);
    }
    public static int LoadPremiumCurrency()
    {
        return PlayerPrefs.GetInt("PremiumCurrency");
    }
    #endregion

    #region Level Currency
    public static void SaveTotalLevelCurrency(int _totalCurrency)
    {
        PlayerPrefs.SetInt("LevelCurrency", _totalCurrency);
    }
    public static void SaveLevelCurrency(int _currency)
    {
        PlayerPrefs.SetInt("LevelCurrency", LoadLevelCurrency() + _currency);
    }
    public static int LoadLevelCurrency()
    {
        return PlayerPrefs.GetInt("LevelCurrency");
    }
    #endregion

    #region Generalized Currency
    public static void SaveTotalCurrency(Currency _currency, BigInteger _totalCurrency)
    {

        PlayerPrefs.SetString(_currency.ToString() + "Currency", _totalCurrency.ToString());
    }
    public static void SaveCurrency(Currency _currency, BigInteger _currencyStep)
    {
        PlayerPrefs.SetString(_currency.ToString() + "Currency", (LoadCurrency(_currency) + _currencyStep).ToString());
    }
    public static BigInteger LoadCurrency(Currency _currency)
    {
        if (!PlayerPrefs.HasKey(_currency.ToString() + "Currency"))
        {
            SaveTotalCurrency(_currency, 0);
        }
        return BigInteger.Parse(PlayerPrefs.GetString(_currency.ToString() + "Currency"));
    }
    #endregion

    #region LevelStates
    public static void SaveCurrentLevel(int _level)
    {
        PlayerPrefs.SetInt("CurrentLevel", _level);
    }

    public static int LoadCurrentLevel()
    {
        if(!PlayerPrefs.HasKey("CurrentLevel"))
        {
            SaveCurrentLevel(0);
        }
        return PlayerPrefs.GetInt("CurrentLevel");
    }

    public static void SaveSoldTrees(int _total)
    {
        PlayerPrefs.SetInt("SoldTrees", _total);
    }

    public static int LoadSoldTrees()
    {
        if(!PlayerPrefs.HasKey("SoldTrees"))
        {
            SaveSoldTrees(0);
        }
        return PlayerPrefs.GetInt("SoldTrees");
    }
    #endregion

    #region Upgrades
    public static void SaveTotalUpgradeLevel(UpgradeType _type, int _level)
    {
        PlayerPrefs.SetInt("Upgrade"+_type+"Level", _level);

    }
    public static int LoadTotalUpgradeLevel(UpgradeType _type)
    {
        if (!PlayerPrefs.HasKey("Upgrade" + _type + "Level"))
        {
            SaveTotalUpgradeLevel(_type,0);
        }
        return PlayerPrefs.GetInt("Upgrade" + _type + "Level");
    }
    #endregion

    #region Player
    public static void SavePlayerExperience(float _step)
    {
        if(!PlayerPrefs.HasKey("PlayerExperience"))
        {
            PlayerPrefs.SetFloat("PlayerExperience", 0);
        }
        PlayerPrefs.SetFloat("PlayerExperience", PlayerPrefs.GetFloat("PlayerExperience")+_step);
    }
    public static float LoadPlayerExperience()
    {
        if(!PlayerPrefs.HasKey("PlayerExperience"))
        {
            PlayerPrefs.SetFloat("PlayerExperience", 0);
        }
        return PlayerPrefs.GetFloat("PlayerExperience");
    }
    #endregion

    #region Investments
    public static void ResetInvestments()
    {
        PlayerPrefs.SetString("LeafInvestment", "0");
        PlayerPrefs.SetString("BranchInvestment", "0");
        PlayerPrefs.SetString("FruitInvestment", "0");
    }

    public static void SaveInvestment(string type, BigInteger value)
    {
        PlayerPrefs.SetString(type + "Investment", value.ToString());
    }

    public static BigInteger GetInvestmentValue(string type)
    {
        return BigInteger.Parse(PlayerPrefs.GetString(type + "Investment"));
    }
    #endregion
}
