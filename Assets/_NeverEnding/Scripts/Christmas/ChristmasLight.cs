using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristmasLight : MonoBehaviour
{
    public Leaf leaf;
    public bool isActive;
    public bool isDone;

    public bool CanGrow
    {
        get
        {
            return leaf.isDone;
        }
    }
    private void Start()
    {
        SetActive(false);
    }
    public void SetActive(bool _isActive)
    {
        isActive = _isActive;
        isDone = _isActive;
        GetComponent<MeshRenderer>().enabled= _isActive;
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled= _isActive;
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        GetComponent<Renderer>().GetPropertyBlock(mpb);
        ColorHSV hsvColor = transform.GetChild(0).GetComponent<ChristmasLightColor>().sphereColor.ToHSV();
        hsvColor.s *= .3f;
        mpb.SetColor("_ColorGradient", hsvColor.ToRGB());
        GetComponent<Renderer>().SetPropertyBlock(mpb);
    }
}
