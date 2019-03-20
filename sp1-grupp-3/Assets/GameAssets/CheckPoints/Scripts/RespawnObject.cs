using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    public event Action<GameObject> OnMove;
    protected void TriggerMove(GameObject obj) { OnMove(obj); }
}

public class RespawnObject : MonoBehaviour
{
    [SerializeField] private LevelChanger lc;
    [SerializeField]
    RespawnTrigger RespawnTrigger;
    PlayerMovement playerMove;
    PlayerJump playerJump;
    public float timer;
    bool respawn = false;

    public Transform checkpoint;

    void Start()
    {
        RespawnTrigger = GetComponent<RespawnTrigger>();
        playerMove = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
        RespawnTrigger.OnMove += Move;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            lc.FadeToLevel(1);
        }

        if (respawn)
            Move(gameObject);
    }

    private void Move(GameObject obj)
    {
        respawn = true;
        playerMove.enabled = false;
        playerJump.enabled = false;

        if (timer <= 0)
        {
            obj.transform.position = checkpoint.position;
            timer = 2;
            respawn = false;
            playerMove.enabled = true;
            playerJump.enabled = true;
        }
        else
            timer -= Time.deltaTime;
    }
}