using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pool : MonoBehaviour
{
    [Header("Instantiate on Start")]
    public bool willInstantiate;
    public int startingPool;
    [Header("Pool Set Up")]
    public GameObject poolObject;
    public Transform parent;
    public List<GameObject> pool;
    

    private void Start()
    {
        if(willInstantiate)
        {
            InstantiatePool();
        }
    }


    public void InstantiatePool()
    {
        for(int i=pool.Count; i<startingPool; i++)
        {
            GameObject go = Instantiate(poolObject, parent);
            pool.Add(go);
            go.SetActive(false);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pool.Count; i++)
        {
            if(!pool[i].activeSelf)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        GameObject go = Instantiate(poolObject, parent);
        pool.Add(go);
        return go;
    }

    public void DisablePool()
    {
        for(int i=0; i<pool.Count; i++)
        {
            pool[i].SetActive(false);
        }
    }
    
}
