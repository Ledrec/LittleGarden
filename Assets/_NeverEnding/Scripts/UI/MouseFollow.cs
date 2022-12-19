using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    readonly int touchAnimation = Animator.StringToHash("Touch");

    Vector3 pos;
    Animator anim;
    public float speed;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        pos = Input.mousePosition;
        pos.z = speed;
        transform.position = Camera.main.ScreenToWorldPoint(pos);

        if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger(touchAnimation);
            Debug.Log("Pressed primary button.");
        }
    }
}