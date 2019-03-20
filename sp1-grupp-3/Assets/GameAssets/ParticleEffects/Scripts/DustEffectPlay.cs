using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustEffectPlay : MonoBehaviour
{
    ParticleSystem particle;
    GroundCheck grounded;
    public float emmisionRate;
    ParticleSystem.EmissionModule emission;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        grounded = GetComponentInParent<GroundCheck>();
        emission = particle.emission;

    }

    void Update()
    {

        if (grounded.isGrounded) {
            emission.rateOverDistance = emmisionRate;

        }
        else if (!grounded.isGrounded) {
            emission.rateOverDistance = 0f;
        }
    }
}
