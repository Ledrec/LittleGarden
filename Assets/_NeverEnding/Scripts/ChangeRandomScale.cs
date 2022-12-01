using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRandomScale : MonoBehaviour
{
    public Vector2 minMaxScale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScale()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            float scale = Random.Range(minMaxScale.x, minMaxScale.y);
            transform.GetChild(i).localScale = new Vector3(scale, scale, scale);
        }
    }
}
