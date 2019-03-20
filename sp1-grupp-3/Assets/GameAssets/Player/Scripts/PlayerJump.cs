using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public MovementState newMovementState;
    public MovementState defaultMovementState;

    [Space]
    float jumpHeight = 0;
    float jumpSustain;
    
    float fallGravity = 3;
    float tapJumpGravity = 2;

    float maxFall;
    float jumpLengthTimer = 0;
    bool jumpRequest = false;

    GroundCheck gCheck;
    Rigidbody2D rb;

    bool hasBeenGrounded = false;
    public bool enableNewMovement = false;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gCheck = GetComponent<GroundCheck>();
        anim = GetComponent<Animator>();
    }

    public void UpdateMovementState(MovementState move)
    {
        jumpHeight = move.jumpHeight;
        jumpSustain = move.jumpSustain;
        maxFall = move.maxFall;
        fallGravity = move.fallGravity;
        tapJumpGravity = move.tapJumpGravity;
    }

    void Update()
    {
        if (newMovementState != null && enableNewMovement == true)
        {
            UpdateMovementState(newMovementState);
        }
        else if (newMovementState == null || newMovementState != null && enableNewMovement == false)
        {
            UpdateMovementState(defaultMovementState);
        }

        if (hasBeenGrounded == false)
        { hasBeenGrounded = gCheck.isGrounded; }

        if(hasBeenGrounded == true)
        {
            jumpLengthTimer = 0;
        }

        bool jumpBtn = Input.GetAxisRaw("Jump") == 1 || Input.GetButton("ABtn") || Input.GetAxisRaw("CtrlJump") == 1;
        if (Input.GetButtonDown("Jump") && gCheck.isGrounded == true || jumpBtn == true && gCheck.isGrounded == false && hasBeenGrounded == true)
        {
            anim.SetBool("Jumped", true);
            jumpRequest = true;
            hasBeenGrounded = false;
            
            //gCheck.isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        if (jumpRequest == true)
        {
            //anim.SetBool("Jumped", true); /*jumpRequest = true;*/

            rb.velocity = new Vector2(rb.velocity.x, 0f);
            var yVel = jumpHeight;

            if (rb.velocity.y < 0)
            {
                var vel = rb.velocity;
                vel.y = yVel;
                rb.velocity = vel;
            }
            else
            {
                rb.velocity += new Vector2(0, yVel);
            }

            anim.SetBool("Jumped", false);
            jumpRequest = false;
        }

        if(rb.velocity.y < 0)
        {
            Vector2 y = rb.velocity;

            if(y.y < maxFall)
            {
                y.y = maxFall;
            }
            rb.velocity = y;
        }
       
        ApplyJumpModifier();
    }

    void ApplyJumpModifier()
    {
        bool jumpBtn = Input.GetAxisRaw("Jump") == 1 || Input.GetButton("ABtn") || Input.GetAxisRaw("CtrlJump") == 1;

        if (jumpBtn == true)
        {
            jumpLengthTimer += Time.deltaTime;
        }

        if (rb.velocity.y < -0.1f && gCheck.isGrounded == false || jumpLengthTimer > jumpSustain)
        {
            rb.gravityScale = fallGravity;
        }
        else if (rb.velocity.y > 0.1f && gCheck.isGrounded == false && !jumpBtn)
        {
            rb.gravityScale = tapJumpGravity;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }
}