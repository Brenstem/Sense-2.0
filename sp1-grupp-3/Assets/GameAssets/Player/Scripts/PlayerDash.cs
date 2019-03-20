using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDash : MonoBehaviour
{
    public event Action OnDash;

    private bool isDashing = false;
    private Vector2 currentBoostMax;
    private float currentBoostTime;

    //Lerp boost
    [SerializeField]
    private float dashDuration = 0.5f;

    [SerializeField]
    private Vector2 dashSpeedBoost;
    private Vector2 lastBoost;
    private Rigidbody2D rb;
    private GroundCheck gCheck;
    public float maxBoostSpeed;

    private bool hasDashed = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gCheck = GetComponent<GroundCheck>();
    }

    private void Update()
    {
        if (hasDashed && gCheck.isGrounded)
            hasDashed = false;

        if (Input.GetButtonDown("Dash") && !gCheck.isGrounded && !hasDashed)
        {
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerJump>().enabled = false;
            DoDash();
        }


        if (currentBoostMax.sqrMagnitude <= 0)
            return;

        Apply();

        if (currentBoostTime / dashDuration >= 1)
            ResetDash();
    }

    public void ResetDash()
    {
        currentBoostTime = dashDuration;
        Apply();

        currentBoostMax = Vector2.zero;
        lastBoost = Vector2.zero;

        currentBoostTime = 0;

        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerJump>().enabled = true;
        hasDashed = true;
    }

    private void DoDash()
    {

        if (currentBoostMax.sqrMagnitude <= 0)
            currentBoostMax += Vector2.right * maxBoostSpeed;//currentBoostMax += Vector2.right * rb.velocity.x;
        currentBoostMax += dashSpeedBoost;


        hasDashed = true;
        OnDash();
    }
    private void Apply()
    {
        currentBoostTime += Time.deltaTime;
        var newBoost = Vector2.Lerp(currentBoostMax, Vector2.zero, currentBoostTime / dashDuration);

        var dash = newBoost - lastBoost;

        var vel = rb.velocity;
        vel.x += dash.x * transform.localScale.x;
        vel.y = newBoost.y;
        rb.velocity = vel;

        lastBoost = newBoost;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag != "Platform")
        {
            return;
        }

        //if (!collision.contacts.(c => c.normal.x != 0))
        //return;

        ResetDash();
        rb.velocity = Vector2.zero;
    }
}