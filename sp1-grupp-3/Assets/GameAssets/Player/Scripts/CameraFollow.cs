using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject cameraTarget;

    public float cameraSpeed = 0.125f;
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public float moveToDelayTime; //Time before moving to new target

    Vector3 velocity;
    GameObject player;
    float moveToDelayTimer = 0;

    void Start()
    {
        moveToDelayTimer = moveToDelayTime;
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        moveToDelayTimer += Time.deltaTime;

        if (cameraTarget == null) {
            cameraTarget = player;
        }

        if (moveToDelayTimer > moveToDelayTime)
        {
            if (cameraTarget != null)
            {
                CameraFollower(cameraTarget.transform.position);
            }
            moveToDelayTimer = moveToDelayTime + 1;
        }
    }

    void CameraFollower(Vector3 target)
    {
        Vector3 desiredPosition = target + offset;

        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, cameraSpeed);
        transform.position = smoothedPosition;
    }
}