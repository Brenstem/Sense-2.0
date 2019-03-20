using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideWallFade : MonoBehaviour
{
    [SerializeField] string playerTag;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag(playerTag))
        {
            animator.SetBool("Activated", true);
        }    
    }

    private void OnTriggerExit2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag(playerTag))
        {
            animator.SetBool("Activated", false);
        }
    }
}
