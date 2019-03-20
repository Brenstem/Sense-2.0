using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovementChange : MonoBehaviour
{
    [SerializeField] string playerTag;
    [SerializeField] bool movementEnabled; 

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag(playerTag))
        {
            hitInfo.GetComponent<PlayerMovement>().enableNewMovement = movementEnabled;
            hitInfo.GetComponent<PlayerJump>().enableNewMovement = movementEnabled;
        }
    }
}
