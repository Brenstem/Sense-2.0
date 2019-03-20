using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    [Header("Add tiles as children to this gameobject. Do not give the tiles colliders.")]

    [SerializeField] float climbSpeed;
    [SerializeField] string playerTag;

    private GameObject player;
    private Vector2 moveDir;

    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector2(0, Input.GetAxisRaw("Vertical"));

        if (player != null)
        {
            if (moveDir.y != 0)
            {
                if (player.GetComponent<GroundCheck>().isGrounded)
                {
                    player.GetComponent<PlayerMovement>().enabled = true;
                    player.GetComponent<PlayerJump>().enabled = true;
                    player.GetComponent<Rigidbody2D>().gravityScale = 1;
                }
            }
        } 
    }

    private void OnTriggerStay2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag(playerTag))
        {
            player = hitInfo.gameObject;

            if (player.GetComponent<PlayerGrab>().grabbedBox == false)
            {
                if (!player.GetComponent<GroundCheck>().isGrounded)
                {
                    player.GetComponent<PlayerMovement>().enabled = false;
                    player.GetComponent<PlayerJump>().enabled = false;
                    player.GetComponent<Rigidbody2D>().gravityScale = 0;
                }
                player.GetComponent<Rigidbody2D>().velocity = moveDir * climbSpeed;
            }
        }    
    }

    private void OnTriggerExit2D(Collider2D hitInfo)
    {
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerJump>().enabled = true;
            player.GetComponent<Rigidbody2D>().gravityScale = 1;
            player = null;
        }
    }
}
