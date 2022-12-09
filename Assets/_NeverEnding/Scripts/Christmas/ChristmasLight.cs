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
        //mpb.SetColor("_ColorGradient", transform.GetChild(0).GetComponent<ChristmasLightColor>().sphereColor.ToHSV());
        GetComponent<Renderer>().SetPropertyBlock(mpb);


    }
}
