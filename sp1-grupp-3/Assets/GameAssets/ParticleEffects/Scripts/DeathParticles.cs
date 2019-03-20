using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class DeathParticles : MonoBehaviour
{
    ParticleSystem particles;
    StudioEventEmitter soundEvent;

    void Start()
    {
        Death death = GetComponentInParent<Death>();
        soundEvent = GetComponent<StudioEventEmitter>();
        particles = GetComponent<ParticleSystem>();

        death.OnPlayerDeath += PlayerDeath;
    }

    private void PlayerDeath()
    {
        particles.Play();
        soundEvent.Play();
    }
}
