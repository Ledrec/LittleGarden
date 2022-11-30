using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using UnityEngine;
// using Cinemachine;

public static class Extensions
{
    // Gotten from:
    // https://answers.unity.com/questions/530178/how-to-get-a-component-from-an-object-and-add-it-t.html?page=2
    private const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
    
    public static T GetCopyOf<T>(this Component comp, T other, params string[] ignore) where T : Component
    {
        Type type = comp.GetType();
        if (type != other.GetType()) return null; // type mis-match

        List<Type> derivedTypes = new List<Type>();
        Type derived = type.BaseType;
        while(derived != null)
        {
            if(derived == typeof(MonoBehaviour))
            {
                break;
            }
            derivedTypes.Add(derived);
            derived = derived.BaseType;
        }

        IEnumerable<PropertyInfo> pinfos = type.GetProperties(bindingFlags);

        foreach (Type derivedType in derivedTypes)
        {
            pinfos = pinfos.Concat(derivedType.GetProperties(bindingFlags));
        }

        pinfos = from property in pinfos
            where Array.IndexOf(ignore, property.Name) == -1
            where !property.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(ObsoleteAttribute))
            select property;
        foreach (var pinfo in pinfos)
        {
            if (pinfo.CanWrite)
            {
                if (pinfos.Any(e => e.Name == $"shared{char.ToUpper(pinfo.Name[0])}{pinfo.Name.Substring(1)}"))
                {
                    continue;
                }
                try
                {
                    pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                }
                catch { } // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
            }
        }

        IEnumerable<FieldInfo> finfos = type.GetFields(bindingFlags);

        foreach (var finfo in finfos)
        {

            foreach (Type derivedType in derivedTypes)
            {
                if (finfos.Any(e => e.Name == $"shared{char.ToUpper(finfo.Name[0])}{finfo.Name.Substring(1)}"))
                {
                    continue;
                }
                finfos = finfos.Concat(derivedType.GetFields(bindingFlags));
            }
        }

        foreach (var finfo in finfos)
        {
            finfo.SetValue(comp, finfo.GetValue(other));
        }

        finfos = from field in finfos
                where field.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(ObsoleteAttribute))
                select field;
        foreach (var finfo in finfos)
        {
            finfo.SetValue(comp, finfo.GetValue(other));
        }

        return comp as T;
    }

    public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component => go.AddComponent(toAdd.GetType()).GetCopyOf(toAdd) as T;
    
    // public static float GetNormalProgress(this CinemachineBlend blend) => (blend.TimeInBlend / blend.Duration);
    
    public static ColorHSV ToHSV(this Color color)
    {
        float _h, _s, _v;
        Color.RGBToHSV(color, out _h, out _s, out _v);
        return new ColorHSV(_h, _s, _v);
    }
    
    public static void DestroyChildren(this Transform t) { foreach (Transform child in t) { UnityEngine.Object.Destroy(child.gameObject); } }
    
    public static void DestroyChildrenImmediate(this Transform t) { foreach (Transform child in t) { UnityEngine.Object.DestroyImmediate(child.gameObject); } }
    
    public static void AssignOrCreateComponentWhenEmpty<T>(this MonoBehaviour script, ref T variable) where T : Component
    {
        if (variable == null)
        {
            variable = script.GetComponent<T>();
            if (variable == null) { variable = script.gameObject.AddComponent<T>(); }
        }
    }
    
    public static string ToCompactString(this int _n)
    {
        if (_n <= 0) { return "0"; }
        int _digits = (int)Mathf.Floor(Mathf.Log10(_n)) + 1;
        int _sections = (_digits - 1) / 3;
        float _short = (float)_n / Mathf.Pow(1000, _sections);
        return $"{_short.ToString("0.#")} {shortDigitName[Mathf.Min(_sections, shortDigitName.Length - 1)]}";
    }
    
    public static string ToCompactString(this float _n, bool _showDecimalsWhenNoLetterIsRequired = false)
    {
        if (_n <= 0.0f) { return "0"; }
        int _digits = (int)Mathf.Floor(Mathf.Log10(_n)) + 1;
        int _sections = (_digits - 1) / 3;
        float _short = (float)_n / Mathf.Pow(1000, _sections);
        return $"{_short.ToString(((_sections > 0) | (_showDecimalsWhenNoLetterIsRequired)) ? "0.#" : "0")} {shortDigitName[Mathf.Min(_sections, shortDigitName.Length - 1)]}";
    }
    
    public static string ToCompactString(this BigInteger _n)
    {
        if (_n <= 0) { return "0"; }
        int _sections = (((int)BigInteger.Log10(_n) + 1) - 1) / 3;
        float _short = (_sections == 0) ? (float)_n
                        : (float)(_n / BigInteger.Pow(1000, _sections - 1)) / 1000.0f;
        return $"{_short.ToString((_sections > 0) ? "0.#" : "0")} {shortDigitName[Mathf.Min(_sections, shortDigitName.Length - 1)]}".TrimEnd();
    }
    
    private static readonly string[] shortDigitName = { "", "K", "M", "T", "Q", "Aa", "Bb", "Cc", "Dd", "Ee", "Ff", "Gg", "Hh", "Ii", "A lot" };
}

public class ColorHSV
{
    public float h;
    public float s;
    public float v;
    
    public ColorHSV(float h, float s, float v)
    {
        this.h = h;
        this.s = s;
        this.v = v;
    }
    
    public Color ToRGB() => Color.HSVToRGB(h, s, v);

    public override string ToString() => $"h: {h}, s: {s}, v: {v}.";
}