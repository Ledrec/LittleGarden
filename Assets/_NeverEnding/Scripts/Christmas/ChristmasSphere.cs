using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristmasSphere : MonoBehaviour
{
    readonly int enterAnimation = Animator.StringToHash("Enter");

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
        GetComponent<MeshRenderer>().enabled = _isActive;
        GetComponent<Animator>().SetBool(enterAnimation, _isActive);
    }
}
