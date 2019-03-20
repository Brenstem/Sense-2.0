using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerJump jump;
    private GroundCheck groundCheck;
    private PlayerGrab grab;
    private Animator anim;
    bool climbing = false;
    float movingVertical = 0;
    bool useLever = false;
    float previousDirection;
    
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        jump = GetComponent<PlayerJump>();
        groundCheck = GetComponent<GroundCheck>();
        grab = GetComponent<PlayerGrab>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float moving = 0;

        if(Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("CtrlHorizontal") > 0 || Input.GetAxisRaw("CtrlHorizontal") < 0)
        {
            moving = 1;
        }

        anim.SetFloat("Moving", moving);

        anim.SetBool("Grabbed", grab.grabbedBox);

        anim.SetBool("IsGrounded", groundCheck.isGrounded);

        anim.SetBool("Use", useLever);

        if(jump.enabled == false && movement.enabled == false && useLever == false)
        {
            climbing = true;
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0 || Mathf.Abs(Input.GetAxisRaw("CtrlVertical")) > 0.5f)
            {
                movingVertical = 1;
            }
            else
            {
                movingVertical = 0;
            }
        }
        else
        {
            climbing = false;
            movingVertical = 0;
        }
        
        anim.SetBool("Climbing", climbing);
        anim.SetFloat("MovingVertical", movingVertical);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.GetComponent<Lever>() != null && useLever == false)
        {
            if (collision.transform.GetComponent<Lever>().enabled == true)
            {
                if (Input.GetButton("Use"))
                {
                    useLever = true;
                    movement.enabled = false;
                    jump.enabled = false;
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                    previousDirection = transform.localScale.x;
                    if (transform.localScale.x == 1)
                    {
                        Vector3 scale = transform.localScale;
                        scale.x *= -1;
                        transform.localScale = scale;
                    }
                }
            }
        }

        if (collision.transform.GetComponent<DoorObject>() != null && useLever == false)
        {
            if (collision.transform.GetComponent<DoorObject>().enabled == true)
            {
                if (Input.GetButton("Use"))
                {
                    useLever = true;
                    movement.enabled = false;
                    jump.enabled = false;
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                    previousDirection = transform.localScale.x;
                }
            }
        }
    }

    public void ResetUse()
    {
        useLever = false;
        movement.enabled = true;
        jump.enabled = true;

        if (previousDirection != transform.localScale.x)
        {
            Vector3 scale = transform.localScale;
            scale.x = previousDirection;
            transform.localScale = scale;
        }
    }
}