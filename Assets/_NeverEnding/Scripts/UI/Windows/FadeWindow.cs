using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeWindow : Window
{
    public Image image;
    public float fadeTime;
    public UnityEngine.Events.UnityAction midAction;
    public UnityEngine.Events.UnityAction endAction;

    public Color fadeColor
    {
        get
        {
            return image.color;
        }
        set
        {
            image.color = value;
        }

    }
    
    public override IEnumerator IEShow()
    {
        yield return StartCoroutine(base.IEShow());
        yield return new WaitForSeconds(fadeTime / 2);
        if(midAction!=null)
        {
            midAction();
            midAction = null;
            yield return null;
        }

        yield return base.IEHide();
        yield return new WaitForSeconds(fadeTime / 2);
        
        if(endAction != null)
        {
            endAction();
            endAction = null;
        }

    }
  

}
