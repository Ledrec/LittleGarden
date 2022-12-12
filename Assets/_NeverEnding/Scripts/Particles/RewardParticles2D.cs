using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardParticles2D : MonoBehaviour
{
    public GameObject particlePrefab;
    public Transform parent;
    [Header("Initial Data")]
    public Vector2 quantity;
    public Vector2 startRotation;
    public Vector2 startSpeed;
    public Vector2 rotationSpeed;
    [Header("Lifetime data")]
    public Vector2 lifetime;
    public Gradient colorOverLifetime;
    public AnimationCurve sizeOverLifetime;
    [Header("Sending")]
    public bool willSend;
    public Vector2 sendDelay;
    public Transform target;
    [Header("Shape")]
    public Vector2 elipseSize = new Vector2(1f, 1f);
    private Vector3[] positions;
    public List<GameObject> particles;
    [Header("Shape Visual Aid")]
    public Color lineColor;
    public float lineWidth = 1f;
    public int resolution = 500;
    public float rotationZ = 45;
    private Vector3[] visualPositions;

   
    public void Emit()
    {
        positions = CreateEllipse(elipseSize, transform.position, rotationZ, (int)Random.Range(quantity.x, quantity.y));

        for(int i=0; i<positions.Length-1; i++)
        {
            GameObject go = Instantiate(particlePrefab, positions[i], Quaternion.Euler(Vector3.forward*Random.Range(startRotation.x,startRotation.y)) , parent);
            go.GetComponent<RewardParticle>().lifetime = Random.Range(lifetime.x, lifetime.y);
            go.GetComponent<RewardParticle>().startSpeed = Random.Range(startSpeed.x, startSpeed.y);
            go.GetComponent<RewardParticle>().rotationSpeed = Random.Range(rotationSpeed.x, rotationSpeed.y);
            go.GetComponent<RewardParticle>().colorOverLifetime = colorOverLifetime;
            go.GetComponent<RewardParticle>().sizeOverLifetime = sizeOverLifetime;
            go.GetComponent<RewardParticle>().direction = (positions[i] - transform.position).normalized;
            if(willSend)
            {
                go.GetComponent<RewardParticle>().willSend = willSend;
                go.GetComponent<RewardParticle>().sendDelay = Random.Range(sendDelay.x, sendDelay.y);
                go.GetComponent<RewardParticle>().target = target;
            }
            particles.Add(go);
        }
    }

    #region Visual Aid
    Vector3[] CreateEllipse(Vector2 _elipseSize, Vector3 _elipseCenter, float _rotZ, int _res)
    {

        visualPositions = new Vector3[_res + 1];
        Quaternion q = Quaternion.AngleAxis(_rotZ, Vector3.forward);
        q *= transform.rotation;
        Vector3 center = new Vector3(_elipseCenter.x, _elipseCenter.y, _elipseCenter.z);

        for(int i = 0; i <= _res; i++)
        {
            float angle = (float)i / (float)_res * 2.0f * Mathf.PI;
            visualPositions[i] = new Vector3(_elipseSize.x * Mathf.Cos(angle), _elipseSize.y * Mathf.Sin(angle), 0.0f);
            visualPositions[i] = q * visualPositions[i] + center;
        }

        return visualPositions;
    }

    private void OnDrawGizmosSelected()
    {
        visualPositions = CreateEllipse(elipseSize, transform.position, rotationZ, resolution);

        for(int i = 0; i <= resolution-1; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(visualPositions[i], visualPositions[i + 1]);
        }
    }
    #endregion

}
