using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Window : MonoBehaviour
{
    [Header("Window General")]
    [SerializeField] 
    Animator anmtrWindow;
    public IEnumerator ieShow;
    public IEnumerator ieHide;

    #region ShowHide
    public virtual void Show(bool _useCoroutine = true)
    {
        if(_useCoroutine)
        {
            ieShow = IEShow();
            StartCoroutine(ieShow);
        }
        else
        {
            anmtrWindow.SetBool("isShown", true);

        }
    }

    public virtual IEnumerator IEShow()
    {
        anmtrWindow.SetBool("isShown", true);
        yield return null;

    }

    public virtual void Hide(bool _useCoroutine = true)
    {
        if(_useCoroutine)
        {
            ieHide = IEHide();
            StartCoroutine(ieHide);

        }
        else
        {
            anmtrWindow.SetBool("isShown", false);

        }

    }


    public virtual IEnumerator IEHide()
    {
        anmtrWindow.SetBool("isShown", false);
        yield return null;
    }
    #endregion
}
