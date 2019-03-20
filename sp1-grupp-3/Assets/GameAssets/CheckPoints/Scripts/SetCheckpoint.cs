using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCheckpoint : MonoBehaviour {

    [SerializeField]
    string playerTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag(playerTag))
            return;

        var respawn = collision.GetComponent<RespawnObject>();

        if (respawn == null)
            return;


        if (respawn.checkpoint != null && respawn.checkpoint.position.x > transform.position.x)
            return;

        respawn.checkpoint = transform;

    }
}
