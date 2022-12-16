using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMoneyPrt : MonoBehaviour
{
    public float timeToCounter;
    public Vector2 velMin, velMax;
    Vector3 endPos;
    Vector3 startPos;
    public Transform upperLeft;
    public Transform bottomRight;


    // Start is called before the first frame update
    void OnEnable()
    {
        endPos = Camera.main.ScreenToWorldPoint(UIManager.instance.normalCurrencyCounter.transform.position);
        startPos = transform.position;
    }

    IEnumerator MoveToCounter()
    {

        float eTime = 0;
        while (eTime < timeToCounter)
        {
            eTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, eTime / timeToCounter);
            yield return new WaitForEndOfFrame();
        }
    }
}
