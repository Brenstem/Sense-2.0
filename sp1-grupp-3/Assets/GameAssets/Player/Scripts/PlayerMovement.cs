using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MovementState newMovementState;
    public MovementState defaultMovementState;
    [Space]
    float speed;
    float acceleration;
    float deAcceleration;

    Rigidbody2D rb;
    float velX = 0;
    SpriteRenderer sprite;
    bool stopMoving = false;
    bool facingRight = false;
    public bool enableNewMovement = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
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

        InputHorizontal();
    }

    public void UpdateMovementState(MovementState move)
    {
        speed = move.speed;
        acceleration = move.acceleration;
        deAcceleration = move.deAcceleration;
    }

    void InputHorizontal()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); //Get Input For Keyboard
        float ctrlHorizontal = Input.GetAxisRaw("CtrlHorizontal"); //Get Input For Controller

        GetSetInput(horizontal, ctrlHorizontal);
        GetSetInput(ctrlHorizontal, horizontal);

        Vector2 velocity = new Vector2(velX, rb.velocity.y);

        rb.velocity = velocity;
    }

    void GetSetInput(float input, float secondInput)
    {
        if (Mathf.Abs(input) > 0 && Mathf.Abs(secondInput) <= 0)
        {
            int direction = SetDirection(input); //Make A Variable That Will Store The Direction Of The Player, Depending On Where They Press

            velX = Mathf.MoveTowards(velX, speed * direction, acceleration * Time.deltaTime);
        }
        else if (secondInput == 0)
        {
            velX = Mathf.MoveTowards(velX, 0f, deAcceleration * Time.deltaTime);
        }
    }
    int SetDirection(float value)
    {
        int direction = 0; //Make A Variable That Will Store The Direction Of The Player, Depending On Where They Press
        if (value > 0) //If The Player Pressed "Right"
        {
            if (transform.localScale.x > 0 && stopMoving == true)
            {
                direction = 0;
                velX = 0;
            }
            else
            {
                direction = 1; //Direction Is 1 = Right

                Vector3 scale = transform.localScale;
                scale.x = direction;
                transform.localScale = scale;
            }
        }
        else if (value < 0) //If The Player Pressed "Left"
        {
            if (transform.localScale.x < 0 && stopMoving == true)
            {
                direction = 0;
                velX = 0;
            }
            else
            {
                direction = -1; //Direction Is -1 = Left

                Vector3 scale = transform.localScale;
                scale.x = direction;
                transform.localScale = scale;
            }
        }
        return direction;
    }

    public void StopMovement()
    {
        stopMoving = true;
    }
    public void ContinueMovement()
    {
        stopMoving = false;
    }
}