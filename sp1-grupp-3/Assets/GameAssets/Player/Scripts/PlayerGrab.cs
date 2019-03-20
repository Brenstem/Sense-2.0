using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerGrab : MonoBehaviour
{
    private Transform parentPosition;

    [SerializeField] float followSpeed;
    [SerializeField] float throwForceX;
    [SerializeField] float throwForceY;
    [SerializeField] float rotationSpeed;

    [SerializeField] bool collidingWithBox = false;
    public bool grabbedBox;

    public Vector3 boxCollPosition;
    public Vector3 boxCollSize;
    public LayerMask collideWithBoxLayer;

    GameObject boxGrabbed;
    float parentAngleZ = 0;
    float rotationDestination = 0;
    bool boxIsRotating = true;
    bool boxIsRotated = false;
    bool grabMeNow = false;
    bool dropMeNow = false;
    CapsuleCollider2D currentCollider;

    public Vector2 capsuleOffset;
    public Vector2 capsuleSize;
    public Vector2 capsuleOffsetBox;
    public Vector2 capsuleSizeBox;

    GroundCheck groundCheck;
    PlayerMovement movement;
    PlayerJump jump;
    Rigidbody2D rb;
    float boxPositionX;
    [SerializeField]
    float distanceFromPlayer;
    int currentDirection;

    void Start()
    {
        groundCheck = GetComponent<GroundCheck>();
        movement = GetComponent<PlayerMovement>();
        jump = GetComponent<PlayerJump>();
        rb = GetComponent<Rigidbody2D>();
        currentCollider = GetComponent<CapsuleCollider2D>();
        parentPosition = transform.Find("Parent Position");
        grabbedBox = false;
        boxGrabbed = null;
    }

    void Update()
    {
        float pX = transform.position.x + boxCollPosition.x * transform.localScale.x;
        float pY = transform.position.y + boxCollPosition.y;
        RaycastHit2D[] hitBox = Physics2D.BoxCastAll(new Vector2(pX, pY), boxCollSize, 0f, Vector2.zero, 0f, collideWithBoxLayer);

        collidingWithBox = false;
        foreach(RaycastHit2D h in hitBox)
        {
            //Debug.Log(h.transform.name);
            if (grabbedBox == false && h.transform.GetComponent<GroundCheckAdvanced>().isGrounded == true)
            {
                boxGrabbed = h.transform.gameObject;

                collidingWithBox = true;
            }
            //else
            //{
            //    collidingWithBox = false;
            //}
        }
        //else
        //{
        //    collidingWithBox = false;
        //}

        if (collidingWithBox == true && groundCheck.isGrounded)
        {
            if (Input.GetButtonDown("Grab") && grabbedBox == false)
            {
                currentDirection = (int)transform.localScale.x;
                movement.enabled = false;
                jump.enabled = false;

                Rigidbody2D boxRB = boxGrabbed.GetComponent<Rigidbody2D>();

                rb.velocity = new Vector2(0f, rb.velocity.y);

                boxRB.freezeRotation = true;
                boxRB.bodyType = RigidbodyType2D.Kinematic;
                boxGrabbed.GetComponent<BoxCollider2D>().isTrigger = true;
                currentCollider.offset = capsuleOffsetBox;
                currentCollider.size = capsuleSizeBox;

                if(boxGrabbed.transform.parent != null)
                {
                    boxGrabbed.transform.parent = null;
                }
                boxGrabbed.transform.SetParent(parentPosition.transform);
           
                boxPositionX = transform.position.x + (distanceFromPlayer * currentDirection);
                parentPosition.transform.localPosition = new Vector2(-0.5f, 0f);
                
                grabbedBox = true;
                boxIsRotating = true;
                boxIsRotated = false;
                collidingWithBox = false;
            }
        }

        if (grabbedBox == true && grabMeNow == true)
        {
            if (boxIsRotating == true)
            {
                RotateBox();
            }
            else
            {
                if (Input.GetButtonDown("Grab"))
                {
                    grabbedBox = false;
                }
                else if (!boxGrabbed)
                {
                    grabbedBox = false;
                    return;
                }
            }
        }

        if (grabbedBox == false && dropMeNow == true)
        {
            Drop();
        }
    }

    float GetRotationDestination(float newPoint)
    {
        float newDestination = 0;

        if (newPoint > 0 && newPoint < 90)
        {
            if (newPoint >= 45)
            {
                newDestination = 90;
            }
            else if (newPoint <= 45)
            {
                newDestination = 0;
            }
        }
        else if (newPoint > 90 && newPoint < 180)
        {
            if (newPoint >= 135)
            {
                newDestination = 180;
            }
            else if (newPoint <= 135)
            {
                newDestination = 90;
            }
        }
        else if (newPoint > 180 && newPoint < 360)
        {
            if (newPoint >= 225)
            {
                newDestination = 360;
            }
            else if (newPoint <= 225)
            {
                newDestination = 180;
            }
        }
        else if (newPoint >= 360)
        {
            newDestination = 0;
        }
        if (newPoint > 0)
        {
            if (newDestination < 0)
            {
                newDestination = -newDestination;
            }
        }
        else
        {
            if (newDestination > 0)
            {
                newDestination = -newDestination;
            }
        }

        return newDestination;
    }

    void RotateBox()
    {
        float point = Mathf.Abs(boxGrabbed.transform.rotation.eulerAngles.z);
        rotationDestination = GetRotationDestination(point);

        if (boxGrabbed.transform.parent == null)
        {
            boxGrabbed.transform.SetParent(parentPosition.transform);
            boxPositionX = transform.position.x + (distanceFromPlayer * currentDirection);
        }
        
        if (Mathf.Abs(parentPosition.transform.rotation.z) < 0.7f)
        {
            boxIsRotating = true;

            if (boxIsRotated == false)
            {
                boxGrabbed.transform.position = Vector2.MoveTowards(boxGrabbed.transform.position, new Vector2(boxPositionX, boxGrabbed.transform.position.y), 100f);

                point = Mathf.MoveTowardsAngle(point, rotationDestination, 10f);
                boxGrabbed.transform.rotation = Quaternion.Euler(0f, 0f, point);
                
                if (point == rotationDestination)
                {
                    boxIsRotated = true;
                    boxGrabbed.transform.localPosition = new Vector2(boxGrabbed.transform.localPosition.x, -0.5f);
                }
            }
            else
            {
                parentAngleZ = Mathf.MoveTowards(parentAngleZ, 90f * currentDirection, rotationSpeed);
                parentPosition.transform.rotation = Quaternion.Euler(0f, 0f, parentAngleZ);
            }
        }
        else
        {
            movement.enabled = true;
            jump.enabled = true;
            boxIsRotating = false;
        }
    }

    void Drop()
    {
        var force = new Vector2(throwForceX * (int)transform.localScale.x, throwForceY);

        Rigidbody2D objRB = boxGrabbed.GetComponent<Rigidbody2D>();
        objRB.transform.parent = null;
        objRB.constraints = RigidbodyConstraints2D.None;
        objRB.bodyType = RigidbodyType2D.Dynamic;
        boxGrabbed.GetComponent<BoxCollider2D>().isTrigger = false;
        currentCollider.offset = capsuleOffset;
        currentCollider.size = capsuleSize;
        
        objRB.freezeRotation = false;
        objRB.velocity = force;
        boxGrabbed = null;

        parentPosition.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        parentPosition.transform.localPosition = Vector2.zero;

        parentAngleZ = 0;
        boxIsRotating = true;
        grabMeNow = false;
        boxIsRotated = false;
        dropMeNow = false;
    }

    void OnDrawGizmos()
    {
        if (boxGrabbed != null)
        {
            Gizmos.DrawLine(boxGrabbed.transform.position, (Vector2)boxGrabbed.transform.position + new Vector2(throwForceX * (int)transform.localScale.x, throwForceY));
        }
        float pX = transform.position.x + boxCollPosition.x * transform.localScale.x;
        float pY = transform.position.y + boxCollPosition.y;
        Gizmos.DrawWireCube(new Vector2(pX, pY), boxCollSize);
    }

    public void GrabMePlease()
    {
        grabMeNow = true;
    }
    public void DropMePlease()
    {
        dropMeNow = true;
    }
}