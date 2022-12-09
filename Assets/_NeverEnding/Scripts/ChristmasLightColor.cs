using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristmasLightColor : MonoBehaviour
{
    public Color sphereColor;
    Renderer rend;
    MaterialPropertyBlock mpb;
    // Start is called before the first frame update
    void OnEnable()
    {
        rend = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
        rend.GetPropertyBlock(mpb);
        mpb.SetColor("_hue", sphereColor);
        rend.SetPropertyBlock(mpb);
        GetComponent<Animator>().Play("GlowIdle", 0, Random.Range(0.0f, 1.0f));
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        rend = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
        rend.GetPropertyBlock(mpb);
        mpb.SetColor("_hue", sphereColor);
        rend.SetPropertyBlock(mpb);
    }
#endif

    public void RandomColor()
    {
        Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
        sphereColor = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
        rend = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
        rend.GetPropertyBlock(mpb);
        mpb.SetColor("_hue", sphereColor);
        rend.SetPropertyBlock(mpb);
    }
}
