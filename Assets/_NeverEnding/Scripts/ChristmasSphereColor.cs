using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristmasSphereColor : MonoBehaviour
{
    [Range (0,1)]
    public float sphereColor;
    Renderer rend;
    MaterialPropertyBlock mpb;
    // Start is called before the first frame update
    void OnEnable()
    {
        rend = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
        rend.GetPropertyBlock(mpb);
        mpb.SetFloat("_hue", sphereColor);
        rend.SetPropertyBlock(mpb);
    }

    private void OnValidate()
    {
        rend = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
        rend.GetPropertyBlock(mpb);
        mpb.SetFloat("_hue", sphereColor);
        rend.SetPropertyBlock(mpb);
    }

    public void RandomColor()
    {
        sphereColor = Random.Range(0.0f, 1.0f);
        rend = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
        rend.GetPropertyBlock(mpb);
        mpb.SetFloat("_hue", sphereColor);
        rend.SetPropertyBlock(mpb);
    }
}
