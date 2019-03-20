using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Cameras Speed")]
    public float smoothSpeed = 0.125f;
    [Header("Cameras Offset From The Player")]
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    [Header("Players Prefab Object")]
    public GameObject PrefabPlayer;
    [Header("How Long Will The Camera Wait To Find The New Spawned Player")]
    public float findDelayLength;
    Vector3 velocity;
    GameObject player;
    float findDelayTimer = 0;

    void Start()
    {
        findDelayTimer = findDelayLength;
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        findDelayTimer += Time.deltaTime;
        if (findDelayTimer > findDelayLength)
        {
            if (player != null)
            {
                CameraFollower(player.transform.position);
            }
            findDelayTimer = findDelayLength + 1;
        }
    }

    void CameraFollower(Vector3 target)
    {
        Vector3 desiredPosition = target + offset;

        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }
}