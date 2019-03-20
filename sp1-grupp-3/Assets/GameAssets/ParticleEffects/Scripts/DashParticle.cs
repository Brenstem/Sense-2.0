using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashParticle : MonoBehaviour
{
    ParticleSystem particle;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        PlayerDash dash = GetComponentInParent<PlayerDash>();

        dash.OnDash += onDash;
    }

    private void onDash()
    {
        particle.Play();
    }
}
