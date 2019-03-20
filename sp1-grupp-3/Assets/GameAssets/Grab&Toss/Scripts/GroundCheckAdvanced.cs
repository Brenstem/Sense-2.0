using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckAdvanced : MonoBehaviour
{
    public bool isGrounded = false;
    public bool isWalled = false;
    bool previousWalled = false;
    bool hitPlatform = false;
    bool previousGrounded = false;
    Rigidbody2D rb;
    RaycastHit2D previousHitDown;
 
    public Vector3 groundCollPosition;
    public Vector3 groundCollSizeUD;
    public Vector3 groundCollSizeLR;
    public LayerMask collideWithFloorLayer;
    public LayerMask collideWithWallLayer;
    string hitObjectInfo;
    public float wallDistance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        previousGrounded = isGrounded;
        previousWalled = isWalled;
    }

    void Update()
    {
        RaycastHit2D hitDown = Physics2D.BoxCast(transform.position + new Vector3(0f, -groundCollPosition.y), groundCollSizeUD, 0f, Vector2.zero, 0f, collideWithFloorLayer);

        RaycastHit2D hitUp = Physics2D.BoxCast(transform.position + new Vector3(0f, groundCollPosition.y), groundCollSizeUD, 0f, Vector2.zero, 0f, collideWithWallLayer);

        RaycastHit2D hitRight = Physics2D.BoxCast(transform.position + new Vector3(groundCollPosition.x, 0f), groundCollSizeLR, 0f, Vector2.zero, 0f, collideWithWallLayer);

        RaycastHit2D hitLeft = Physics2D.BoxCast(transform.position + new Vector3(-groundCollPosition.x, 0f), groundCollSizeLR, 0f, Vector2.zero, 0f, collideWithWallLayer);

        CheckDown(hitDown);
        CheckAllOtherSides(hitLeft);
        CheckAllOtherSides(hitRight);
        CheckAllOtherSides(hitUp);
    }

    public bool HasLanded(Vector2 direction , ref bool previous)
    {
        if(IsGrounded(direction))
        {
            if (!previous) //Check If Was Grounded Previous Frame
            {
                previous = true;
                return true; //Has Landed
            }
        }
        else
        {
            previous = false;
        }
        return false; //Isn't Grounded
    }

    bool IsGrounded(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, wallDistance, collideWithWallLayer);
        Debug.DrawRay(transform.position, direction * wallDistance);

        if (hit) //If Grounded
        {
            return true;
        }
        return false;
    }

    void CheckAllOtherSides(RaycastHit2D hit)
    {
        if (hit == true)
        {
            isWalled = true;
            hitObjectInfo = hit.transform.gameObject.layer.ToString();
        }
        else
        {
            isWalled = false;
        }
    }

    public string GetHitInfO()
    {
        return hitObjectInfo;
    }

    public bool IsMovingOnAir()
    {
        if(isGrounded == false)
        {
            if(Mathf.Abs(rb.velocity.x) > 0.1f || Mathf.Abs(rb.velocity.y) > 0.1f)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsDraggedOnGround()
    {
        if (isGrounded == true)
        {
            if (Mathf.Abs(rb.velocity.x) > 0.1f)
            {
                return true;
            }
        }
        return false;
    }

    public bool HasHitWall()
    {
        if(isWalled == true)
        {
            if (previousWalled == false)
            {
                previousWalled = isWalled;
                return true;
            }
        }
        previousWalled = isWalled;
        return false;
    }

    void CheckDown(RaycastHit2D hitDown)
    {
        if (hitDown == true)
        {
            if (hitDown.transform.gameObject.layer == LayerMask.NameToLayer("JumpThroughPlatforms"))
            {
                if (rb.velocity.y < 0 && (transform.position.y + groundCollPosition.y - (groundCollSizeUD.y / 2)) >= (hitDown.transform.position.y - hitDown.transform.GetComponent<BoxCollider2D>().size.y / 4))
                {
                    isGrounded = true;
                    previousHitDown = hitDown;

                    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), hitDown.transform.GetComponent<Collider2D>(), false);
                    hitPlatform = true;
                }
            }
            else
            {
                isGrounded = true;
                hitPlatform = false;
            }
        }
        else
        {
            isGrounded = false;
            if (previousHitDown == true)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), previousHitDown.transform.GetComponent<Collider2D>(), true);
                hitPlatform = false;
                previousHitDown = hitDown;
            }
        }
    }

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(transform.position + new Vector3(0f, groundCollPosition.y), groundCollSizeUD);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + new Vector3(0f, -groundCollPosition.y), groundCollSizeUD);
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireCube(transform.position + new Vector3(groundCollPosition.x, 0f), groundCollSizeLR);
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireCube(transform.position + new Vector3(-groundCollPosition.x, 0f), groundCollSizeLR);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.layer == LayerMask.NameToLayer("JumpThroughPlatforms"))
        {
            if (isGrounded == true && hitPlatform == false || isGrounded == false)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.transform.GetComponent<Collider2D>(), true);
            }
        }
    }
}