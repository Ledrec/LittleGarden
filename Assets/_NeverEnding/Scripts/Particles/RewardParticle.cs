using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardParticle : MonoBehaviour
{
    //public SpriteRenderer spriteRenderer;
    [Header("Initial Data")]
    public float speedCutter=10;
    public float startSpeed;
    public float lifetime;
    public Gradient colorOverLifetime;
    public AnimationCurve sizeOverLifetime;
    public Vector2 direction;
    public float rotationSpeed;
    [Header("Sending")]
    public bool willSend;
    public float sendDelay;
    public Transform target;
    float timer;
    Vector3 startSize;
    Vector3 updatedLastPos;

    private void OnEnable()
    {
        startSize = transform.localScale;
        
    }


    private void Update()
    {
        //spriteRenderer.color = colorOverLifetime.Evaluate(timer / lifetime);
        transform.localScale = startSize * sizeOverLifetime.Evaluate(timer / lifetime);
        transform.eulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;
        if(!willSend||(willSend&&timer<sendDelay))
        {
            transform.position += (Vector3)direction * (startSpeed*speedCutter) * Time.deltaTime;
            updatedLastPos = transform.position;
        }
        else
        {
            transform.position = Vector3.Lerp(updatedLastPos, target.position, Mathf.Clamp01((timer - sendDelay) / (lifetime - sendDelay)));
        }
        if(timer>lifetime)
        {
            Destroy(gameObject);
        }

        timer += Time.deltaTime;

    }


}
