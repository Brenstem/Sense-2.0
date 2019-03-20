using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MovementState : ScriptableObject
{
    [Header("Movement")]
    [Header("Speed Of Which The Player Will Move With")]
    public float speed;
    [Header("Acceleration Towards Speed")]
    public float acceleration;
    [Header("DeAcceleration Towards Zero")]
    public float deAcceleration;

    [Space]
    [Header("Jump")]
    [Header("The Jump Height / Jump Strength")]
    public float jumpHeight;
    [Header("Jump Sustain, Higher Value = More Time In The Air, Low Value = Less Time In Air")]
    public float jumpSustain;
    [Space]
    public float maxFall;
    [Header("Gravity When Falling, Activates When The Player Starts Falling downward, Default Value = 3")]
    public float fallGravity;
    [Header("Gravity For When Tapping The Jump Button, Changes The Gravity When Just Tapping The Jump Button, Default Value = 2")]
    public float tapJumpGravity; 
}