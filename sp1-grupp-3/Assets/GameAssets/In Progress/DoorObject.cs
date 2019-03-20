using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : MonoBehaviour
{
    public string soundPath;

    private bool contact = false;

    private void OnTriggerEnter2D(Collider2D collision) { if (TestCollision(collision)) contact = true; }
    private void OnTriggerExit2D(Collider2D collision) { if (TestCollision(collision)) contact = false; }

    private void Update()
    {
        if (Input.GetButtonDown("Use") && contact)
        {
            GetComponent<SoundEvent>().PlayOneShot(soundPath);
        }
    }

    private bool TestCollision(Collider2D collision)
    {
        return collision.transform.CompareTag("Player");
    }

}
