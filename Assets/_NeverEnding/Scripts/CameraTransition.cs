using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public Transform start;
    public Transform end;

    public void SetPosition(float _percent)
    {
        Camera.main.transform.position = Vector3.Lerp(start.position,end.position, _percent);
        Camera.main.transform.rotation = Quaternion.Lerp(start.rotation,end.rotation, _percent);
    }
}
