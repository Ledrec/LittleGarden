using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum fruitType { apple, orange, peach, jewel};

public class Fruit : MonoBehaviour
{
    [Header("Scaling")]
    public Vector2 scaleLimits;
    public Transform scaleParent;
    public float timeToFullyGrow;
    public float timer;
    public float startGrowDelay;
    public bool isDone;
    public bool canGrow;
    public bool flowerDead;
    public Animator flower;
    public float gravity = 9.8f;

    [Header("FruitType")]
    public fruitType typeFruit;
    public GameObject appleObject;
    public GameObject orangeObject;
    public GameObject peachObject;
    public GameObject jewelObject;

    private void OnEnable()
    {
        //flower.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0.0f, 360.0f));
        //flower.transform.LookAt(Camera.main.transform, Vector3.up);
        StartCoroutine(WaitForFlowerDeath());
        ChooseFruit();
    }


    private void Update()
    {
        if (flowerDead)
        {
            Grow();
        }
    }

    IEnumerator WaitForFlowerDeath()
    {
        yield return new WaitUntil(() => canGrow);
        flower.enabled = true;
        yield return new WaitUntil(() => flower.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        yield return new WaitForSeconds(Random.Range(0.1f, 1.0f));
        flowerDead = true;

        flower.enabled = false;
        List<MeshRenderer> petalRenderers = new();
        flower.GetComponentsInChildren(petalRenderers);
        List<float> randomTimeFallPetals = new();
        List<float> petalStartPos = new();
        List<float> petalsRandomRot = new();
        for (int i = 0; i < petalRenderers.Count; i++)
        {
            randomTimeFallPetals.Add(Random.Range(.3f, 2.0f));
            petalStartPos.Add(petalRenderers[i].transform.position.y);
            petalsRandomRot.Add(Random.Range(100, 720));
        }
        float eTime = 0;
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        while (petalRenderers.Count > 0)
        {
            eTime += Time.deltaTime;
            for (int i = 0; i < petalRenderers.Count; i++)
            {
                if (eTime > randomTimeFallPetals[i])
                {
                    petalRenderers[i].transform.Translate(new Vector3(1, -1 , 0) * Time.deltaTime * gravity, Space.World);
                    petalRenderers[i].transform.Rotate( new Vector3(petalsRandomRot[i], petalsRandomRot[i], petalsRandomRot[i]) * Time.deltaTime);
                    petalRenderers[i].GetPropertyBlock(mpb);
                    mpb.SetFloat("_Alpha", petalRenderers[i].transform.position.y / petalStartPos[i]);
                    mpb.SetFloat("_ShadowIntensity", (petalRenderers[i].transform.position.y / petalStartPos[i]) * .8f);
                    petalRenderers[i].SetPropertyBlock(mpb);
                    if (petalRenderers[i].transform.position.y <= 0)
                    {
                        petalRenderers[i].gameObject.SetActive(false);
                        petalRenderers.RemoveAt(i);
                        randomTimeFallPetals.RemoveAt(i);
                        petalStartPos.RemoveAt(i);
                        petalsRandomRot.RemoveAt(i);
                    }
                }

            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void ChooseFruit()
    {
        switch (typeFruit)
        {
            case fruitType.apple:
                appleObject.SetActive(true);
                break;

            case fruitType.orange:
                orangeObject.SetActive(true);
                break;

            case fruitType.peach:
                peachObject.SetActive(true);
                break;
            case fruitType.jewel:
                jewelObject.SetActive(true);
                break;
        }
    }

    void Grow()
    {
        if(!isDone)
        {
            if(canGrow)
            {
                timer+=Time.deltaTime;
            }
            scaleParent.localScale = Vector3.one * Mathf.Lerp(scaleLimits.x, scaleLimits.y, Mathf.Clamp01((timer-startGrowDelay) / timeToFullyGrow));
            if (Mathf.Clamp01((timer - startGrowDelay) / timeToFullyGrow) >= 1)
            {
                isDone = true;
            }
        }
    }
}
