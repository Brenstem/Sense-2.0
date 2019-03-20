using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded = false;
    public bool isWalled = false;
    public bool isCollidingBoxes = false;
    public Vector3 groundCollPosition;
    public Vector3 groundCollSize;
    public Vector3 wallCollPosition;
    public Vector3 wallCollSize;
    public LayerMask collideWithFloorLayer;
    public LayerMask collideWithWallLayer;
    PlayerMovement movement;
    public float slopeFriction;
    Rigidbody2D rb;
    RaycastHit2D hit2;
    bool hitPlatform = false;
    bool previousGrounded = false;

    void Start()
    {
        if (GetComponent<PlayerMovement>() != null)
        {
            movement = GetComponent<PlayerMovement>();
        }
        rb = GetComponent<Rigidbody2D>();
        previousGrounded = isGrounded;
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + groundCollPosition, groundCollSize, 0f, Vector2.zero, 0f, collideWithFloorLayer);

        if (hit == true)
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("JumpThroughPlatforms"))
            {
                if (rb.velocity.y < 0 && (transform.position.y + groundCollPosition.y - (groundCollSize.y / 2)) >= (hit.transform.position.y - hit.transform.GetComponent<BoxCollider2D>().size.y / 4))
                {
                    isGrounded = true;
                    hit2 = hit;

                    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), hit.transform.GetComponent<Collider2D>(), false);
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
            if (hit2 == true)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), hit2.transform.GetComponent<Collider2D>(), true);
                hitPlatform = false;
                hit2 = hit;
            }
        }

        if (isGrounded == true)
        {
            if (hit.collider != null && Mathf.Abs(hit.normal.x) > 0.1f)
            {
                Rigidbody2D body = GetComponent<Rigidbody2D>();
                body.velocity = new Vector2(body.velocity.x - (hit.normal.x * slopeFriction), body.velocity.y);
                
                Vector2 position = transform.position;
                position.y += -hit.normal.x * Mathf.Abs(body.velocity.x) * Time.deltaTime * (body.velocity.x - hit.normal.x > 0 ? 1 : -1);
                transform.position = position;
            }
        }

        if (movement != null)
        {
            var wallHits = new RaycastHit2D[10];
            float positionX = transform.position.x + (wallCollPosition.x * transform.localScale.x);
            float positionY = transform.position.y;
            int wallHitCount = (Physics2D.BoxCast(new Vector2(positionX, positionY), wallCollSize, 0f, Vector2.zero, new ContactFilter2D { useLayerMask = true, layerMask = collideWithWallLayer }, wallHits));
            isWalled = wallHitCount > 0;

            if (isWalled == true)
            {
                movement.StopMovement();
            }
            else
            {
                movement.ContinueMovement();
            }
        }
    }

    public bool HasLanded()
    {
        if(isGrounded == true)
        {
            if(previousGrounded == false)
            {
                previousGrounded = isGrounded;
                return true;
            }
        }
        previousGrounded = isGrounded;
        return false;
    }

    public bool IsDragging()
    {
        if(isGrounded == true)
        {
            if(Mathf.Abs(rb.velocity.x) > 0)
            {
                return true;
            }
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + groundCollPosition, groundCollSize);

        float positionX = transform.position.x + (wallCollPosition.x * transform.localScale.x);
        float positionY = transform.position.y;
        Gizmos.DrawWireCube(new Vector2(positionX, positionY), wallCollSize);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.layer == LayerMask.NameToLayer("JumpThroughPlatforms"))
        {
            if(isGrounded == true && hitPlatform == false || isGrounded == false)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.transform.GetComponent<Collider2D>(), true);
            }
        }
    }
}