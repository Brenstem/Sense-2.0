using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlatform : MonoBehaviour
{
    Rigidbody2D rb;
    Collision2D collisionPlatform;
    Platform platform;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlatformMoving")) {
            float angleThreshhold = 45.0f;
            ContactPoint2D[] contactPoints = collision.contacts;

            foreach (ContactPoint2D cp in contactPoints) {
                float platformAngle = Mathf.Abs(Vector2.Angle(cp.normal, Vector2.up));

                if (platformAngle < angleThreshhold) {
                    platform = collision.gameObject.GetComponent<Platform>();
                    transform.parent = platform.transform;
                }
            }

            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlatformMoving")) {
            platform = null;
            transform.parent = null;
        }
    }

    public Vector2 GetPlatformVelocity()
    {
        Vector2 platformVelocity;

        if (platform != null) {
            platformVelocity = platform.velocity;
        }
        else {
            platformVelocity = Vector2.zero;

        }

        return platformVelocity;
    }

}
