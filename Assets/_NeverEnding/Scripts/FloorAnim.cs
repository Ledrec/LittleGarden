using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorAnim : MonoBehaviour
{

    public float maxAlpha;
    public Vector2 minMaxTimeShown;
    public Vector2 minMaxIdleTime;
    public float timeToShow;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WindBlow());
    }

    IEnumerator WindBlow()
    {
        yield return new WaitForSeconds(Random.Range(minMaxIdleTime.x, minMaxIdleTime.y));
        Material rend = GetComponent<Terrain>().materialTemplate;
        float eTime = 0;
        while (eTime < timeToShow)
        {
            eTime += Time.deltaTime;
            rend.SetFloat("_shineIntensity", Mathf.Lerp(0, maxAlpha, eTime / timeToShow));
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(Random.Range(minMaxTimeShown.x, minMaxTimeShown.y));
        eTime = 0;
        while (eTime < timeToShow)
        {
            eTime += Time.deltaTime;
            rend.SetFloat("_shineIntensity", Mathf.Lerp(maxAlpha, 0, eTime / timeToShow));
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(WindBlow());
    }

}
