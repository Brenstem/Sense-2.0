using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendRobot : MonoBehaviour
{
    public Transform followObject;
    public float maxSpeed = 1;
    public float acceletation = 1;
    public float deAcceletation = 1;
    public float moveThresholdY = 1;

    private Rigidbody2D rb;
    private Animator animator;
    private PlayerMovement playerMovement;
    private bool move = true;
    private bool isGrounded; 
    private float currentSpeed = 0;
    private float moveDirection;
    private Timer timer;
    private bool isWiggling;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = followObject.GetComponent<PlayerMovement>();
        timer = new Timer();
        timer.StartTimer(Random.Range(5, 10));

        AnimatorUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")) {
            float angleThreshhold = 45.0f;
            ContactPoint2D[] contactPoints = collision.contacts;

            foreach (ContactPoint2D cp in contactPoints) {
                float platformAngle = Mathf.Abs(Vector2.Angle(cp.normal, Vector2.up));

                if (platformAngle < angleThreshhold) {
                    isGrounded = true;
                }
            }
        }        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")) {
            isGrounded = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            move = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            move = true;
        }
    }

    private void Update()
    {
        timer.UpdateTimer();


        if (timer.TimerFinished)
        {
            isWiggling = true;
            timer.ResetTimer();
            timer.StartTimer(Random.Range(3, 7));
        }
        else if (!timer.TimerFinished)
        {
            isWiggling = false;
        }

        if (isGrounded) {
            FollowPoint();
        }
    }

    private void LateUpdate()
    {
        AnimatorUpdate();
    }


    private void FollowPoint()
    {
        if (playerMovement != null) {
            maxSpeed = playerMovement.newMovementState.speed;
        }
                
        Accelerate();

        rb.velocity = Vector2.right * currentSpeed * moveDirection;

    }

    private void Accelerate()
    {
        if (move && TestMoveThresholdY()) {
            if (currentSpeed < maxSpeed) {
                currentSpeed += acceletation;
            }
            else {
                currentSpeed = maxSpeed;
            }

            moveDirection = GetMoveDirection();
        }
        else {
            if (currentSpeed > 0) {
                currentSpeed -= deAcceletation;
            }
            else {
                currentSpeed = 0;
            }
        }
    }

    private float GetMoveDirection()
    {
        if ((followObject.position - transform.position).normalized.x > 0.0f) {
            return 1;
        }
        else if ((followObject.position - transform.position).normalized.x < 0.0f) {
            return -1;
        }

        return 0;
    }

    private void AnimatorUpdate()
    {
        animator.SetFloat("Moving", Mathf.Abs(rb.velocity.x));
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsHanging", IsStatic());
        animator.SetBool("Wiggle", isWiggling);
        
        if (moveDirection < 0) {
            transform.localScale = Vector3.one;
        }
        else if (moveDirection > 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private bool TestMoveThresholdY()
    {
        float followObjectY = followObject.position.y;
        float positionY = transform.position.y;

        if (Mathf.Abs(followObjectY - positionY) < moveThresholdY) {
            return true;
        }

        return false;
    }

    private bool IsStatic()
    {
        if (rb.bodyType == RigidbodyType2D.Static)
        {
            return true;
        }
        else
        {
            return false;
        }
    } 
}
