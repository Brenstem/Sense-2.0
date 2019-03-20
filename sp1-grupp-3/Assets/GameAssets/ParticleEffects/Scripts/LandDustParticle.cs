using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandDustParticle : MonoBehaviour
{
    ParticleSystem particle;
    GroundCheck gc;
    bool previousGrounded = false;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        gc = GetComponentInParent<GroundCheck>();
    }

    void Update()
    {
        if (gc.isGrounded) 
        {
            if(previousGrounded == false) {
                particle.Play();
                previousGrounded = gc.isGrounded;
            }
        }
        previousGrounded = gc.isGrounded;
    }
}
