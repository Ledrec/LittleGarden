using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Helper
{

    #region CustomVectors
    public static Vector3 GetVectorX(float _x)
    {
        return Vector3.right * _x;
    }
    public static Vector3 GetVectorY(float _y)
    {
        return Vector3.up * _y;

    }
    public static Vector3 GetVectorZ(float _z)
    {
        return Vector3.forward * _z;

    }
    public static Vector3 GetVector3(float _x, float _y, float _z)
    {
        return (Vector3.right * _x) + (Vector3.up * _y) + (Vector3.forward * _z);
    }

    public static Vector3 GetVector3FromVector2(Vector2 _vector2,bool _x, bool _y, bool _z)
    {
        if(_x & _y)
        {
            return new Vector3(_vector2.x, _vector2.y, 0);
        }
        else if(_x & _z)
        {
            return new Vector3(_vector2.x,0, _vector2.y);
        }
        else if(_x & _y)
        {
            return new Vector3(_vector2.x, _vector2.y, 0);
        }
        else if(_y & _z)
        {
            return new Vector3(0,_vector2.x, _vector2.y);
        }
        else
        {
            return Vector3.zero;
        }
    }

    #endregion

    #region CustomPercentage
    public static float GetPercentFromRange(float _target, float _min, float _max)
    {
        return (_target - _min) / (_max - _min);
    }

    public static float GetPercent(float _target, float _max)
    {
        return (_target * 100) / _max;
    }
    #endregion

    #region Coroutines

    #region CameraRelated
    public static IEnumerator CameraVibrate()
    {
        Vector3 startPos = Camera.main.transform.localPosition;
        for(int i = 0; i < 7; i++)
        {
            Camera.main.transform.localPosition = new Vector3(Random.Range(startPos.x - .2f, startPos.x + .2f), Random.Range(startPos.y - .2f, startPos.y + .2f), startPos.z);
            yield return new WaitForSeconds(0.02f);
        }
    }
    #endregion

    #region ActiveState
    public static IEnumerator IEChangeActiveState(GameObject _go, bool _state, float _delay)
    {
        yield return new WaitForSeconds(_delay);
        _go.SetActive(_state);
    }
    #endregion

    #region Fade
    public static IEnumerator IEFade(UnityEngine.UI.Image _img, bool _out, float _timeToFade)
    {
        Color start = _out ? Color.white : Color.clear;
        float timer = 0;
        _img.color = start;

        while(timer < _timeToFade)
        {
            _img.color = Color.Lerp(start, _out ? Color.clear : Color.white, timer / _timeToFade);
            timer += Time.deltaTime;
            yield return null;
        }
        _img.color = _out ? Color.clear : Color.white;
    }

    public static IEnumerator IEFade(SpriteRenderer _img, bool _out, float _timeToFade)
    {
        Color start = _out ? Color.white : Color.clear;
        float timer = 0;
        _img.color = start;

        while(timer < _timeToFade)
        {
            _img.color = Color.Lerp(start, _out ? Color.clear : Color.white, timer / _timeToFade);
            timer += Time.deltaTime;
            yield return null;
        }
        _img.color = _out ? Color.clear : Color.white;
    }

    #endregion

    #region Movement
    public static IEnumerator IEMove(GameObject _go, Vector3 _newPos, float _timeToMove)
    {
        Vector3 start = _go.transform.position;
        float timer = 0;

        while(timer < _timeToMove)
        {
            _go.transform.position = Vector3.Lerp(start, _newPos, timer / _timeToMove);
            timer += Time.deltaTime;
            yield return null;
        }
        _go.transform.position = _newPos;


    }
    public static IEnumerator IEMoveRotate(GameObject _go, Vector3 _newPos, Quaternion _newRot, float _timeToMove)
    {
        Vector3 startPos = _go.transform.position;
        Quaternion startRot = _go.transform.rotation;
        float timer = 0;

        while(timer < _timeToMove)
        {
            _go.transform.position = Vector3.Lerp(startPos, _newPos, timer / _timeToMove);
            _go.transform.rotation = Quaternion.Lerp(startRot, _newRot, timer / _timeToMove);
            timer += Time.deltaTime;
            yield return null;
        }
        _go.transform.position = _newPos;
        _go.transform.rotation = _newRot;
    }

    #endregion

    #region Slider

    public static IEnumerator IEMoveSliderValue(UnityEngine.UI.Slider _slider, float _value, float _timeToChange, float maxValue = 0)
    {
        float timer = 0;
        float start = _slider.value;
        while(timer < _timeToChange)
        {
            _slider.value = Mathf.Lerp(start, _value, timer / _timeToChange);
            timer += Time.deltaTime;
           
            yield return null;
        }
        _slider.value = _value;
    }

    #endregion

    #region Text 
    public static IEnumerator IEIncrementTextValue(TMPro.TextMeshProUGUI _txt, int _a, int _b, float _time)
    {
        float timer = 0;
        float value = _a;
        _txt.text = value.ToString("0");

        while(timer<_time)
        {
            value = Mathf.Lerp(_a, _b, timer / _time);
            _txt.text = value.ToString("0");
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        _txt.text = _b.ToString("0");
    }
    public static IEnumerator IEIncrementTextValue(TMPro.TextMeshProUGUI _txt, int _a, int _b, float _time, float _delay)
    {
        yield return new WaitForSeconds(_delay);
        float timer = 0;
        float value = _a;
        _txt.text = value.ToString("0");

        while(timer < _time)
        {
            value = Mathf.Lerp(_a, _b, timer / _time);
            _txt.text = value.ToString("0");
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        _txt.text = _b.ToString("0");
    }

    public static IEnumerator IETextWriter(TMPro.TextMeshProUGUI _txtObject, string _textToType, float _timePerLetter)
    {
        _txtObject.text = "";
        foreach(char letter in _textToType.ToCharArray())
        {
            _txtObject.text += letter;
            yield return 0;
            yield return new WaitForSeconds(_timePerLetter);
        }
    }
    #endregion

    #region Scaling
    public static IEnumerator IEScale(GameObject _go, Vector3 _newScale, float _timeToMove)
    {
        Vector3 start = _go.transform.localScale;
        float timer = 0;

        while(timer < _timeToMove)
        {
            _go.transform.localScale = Vector3.Lerp(start, _newScale, timer / _timeToMove);
            timer += Time.deltaTime;
            yield return null;
        }
        _go.transform.localScale = _newScale;


    }
    public static IEnumerator IEScale(GameObject _go, Vector3 _from, Vector3 _to, float _timeToMove)
    {
        Vector3 start =_from;
        float timer = 0;

        while(timer < _timeToMove)
        {
            _go.transform.localScale = Vector3.Lerp(start, _to, timer / _timeToMove);
            timer += Time.deltaTime;
            yield return null;
        }
        _go.transform.localScale = _to;

    }
    #endregion

    #endregion


    #region Data
    public static int GetNonRepeatedNumber(ref List<int> _list, int _min, int _max)
    {
        int rand = Random.Range(_min, _max);
        bool repeat = false;
        for(int i = 0; i < _list.Count; i++)
        {
            if(_list[i] == rand)
            {
                repeat = true;
                break;
            }
        }
        if(_list.Count == 0)
        {
            _list.Add(rand);
            return rand;
        }
        else if(!repeat)
        {
            _list.Add(rand);
            return rand;
        }
        else
        {
            return GetNonRepeatedNumber(ref _list, _min, _max);
        }
    }
    #endregion
}
