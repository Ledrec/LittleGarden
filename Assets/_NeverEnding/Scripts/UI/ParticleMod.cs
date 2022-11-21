using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ParticleMod : MonoBehaviour
{
    ParticleSystem pSystem;
    int numParticles;
    public Vector2 numParticlesLimits;
    [Header("Movement")]
    public bool wiilMove;
    public Transform target;
    public float startDelay = 1;
    public Vector2 betweenDelay;
    public float timeToMove;
    ParticleSystem.Particle[] particles;

    private void Awake()
    {
        pSystem = GetComponent<ParticleSystem>();

    }
    private void OnEnable()
    {

    }
    public void MoveParticles()
    {
        numParticles = (int)Random.Range(numParticlesLimits.x, numParticlesLimits.y);
        pSystem.Emit(numParticles);
        StartCoroutine(IEMoveParticles());
    }
    IEnumerator IEMoveParticles()
    {
        particles = new ParticleSystem.Particle[numParticles];
        yield return new WaitForSeconds(startDelay);

        pSystem.GetParticles(particles);
        List<Vector3> startPos = new List<Vector3>();

        for(int i = 0; i < particles.Length; i++)//SetUp
        {
            startPos.Add(particles[i].position);
            particles[i].remainingLifetime = timeToMove;
            pSystem.SetParticles(particles);
        }
        Vector3 temp = target.position;//Offsetting postion
        if(wiilMove)
        {
            for(int i = 0; i < particles.Length; i++)
            {
                StartCoroutine(IEMoveParticle(i, startPos[i], temp));
                yield return new WaitForSeconds(Random.Range(betweenDelay.x, betweenDelay.y));
            }

        }

    }

    IEnumerator IEMoveParticle(int _particle, Vector3 _position, Vector3 _goal)
    {
        float timer = 0;
        while(timer < timeToMove)//Movement
        {
            particles[_particle].angularVelocity = 0.0f;
            particles[_particle].velocity = Vector3.zero;
            particles[_particle].position = Vector3.Lerp(_position, _goal, timer / timeToMove);


            pSystem.SetParticles(particles);
            timer += Time.deltaTime;
            yield return null;
        }
        particles[_particle].position = _goal;
        pSystem.SetParticles(particles);
    }

}
