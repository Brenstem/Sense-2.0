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
            hitInfo.GetComponent<PlayerMovement>().enableNewMovement = !hitInfo.GetComponent<PlayerMovement>().enableNewMovement;
            hitInfo.GetComponent<PlayerJump>().enableNewMovement = !hitInfo.GetComponent<PlayerJump>().enableNewMovement;
        }
    }
}
