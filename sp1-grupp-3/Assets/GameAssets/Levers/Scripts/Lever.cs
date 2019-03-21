using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] bool playSound;
    public string soundPath;

    public LeverObject[] affectedObjectArray;
    public float animationSpeed = 1;

    public GameObject cameraPosition;
    public float holdPositionTime;
    private float holdTimer = 0f;
    CameraFollow cameraFollow;

    private GroundCheck groundCheck;

    private bool contact = false;
    private bool leverActivated = false;

    private void OnTriggerEnter2D(Collider2D collision) { if (TestCollision(collision)) contact = true; }
    private void OnTriggerExit2D(Collider2D collision) { if (TestCollision(collision)) contact = false; }



    private void Start()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        groundCheck = GameObject.FindGameObjectWithTag("Player").GetComponent<GroundCheck>();
    }
    private void Update()
    {
        if (Input.GetButton("Use") && contact && !leverActivated && groundCheck.isGrounded) {
            OnPullLever();
            GetComponent<BoxCollider2D>().enabled = false;
        }

        CameraHoldTimer();
    }


    private void OnPullLever()
    {
        if (playSound)
        {
            GetComponent<SoundEvent>().PlayOneShot(soundPath);
            playSound = false;
        }

        if (!leverActivated) {
            Animator animator = GetComponent<Animator>();
            animator.SetFloat("AnimationSpeedParameter", animationSpeed);
            MoveCamera();

            foreach (LeverObject affectedObject in affectedObjectArray) {
                if (affectedObject != null)
                {
                    if (!affectedObject.ActionPerformed)
                    {
                        
                        affectedObject.OnActivatedByLever();
                    }
                }
            }

            leverActivated = true;
            if (leverActivated == true){
            }
        }        
    }

    private bool TestCollision(Collider2D collision)
    {
        return collision.transform.CompareTag("Player");
    }

    private bool TestActivated()
    {
        foreach (LeverObject affectedObject in affectedObjectArray) {
            if (affectedObject.ActionPerformed) {
                Debug.Log("Object has already been activated!");
                return true;
            }
        }

        return false;
    }

    private void MoveCamera()
    {
        if (cameraPosition != null) {
            holdTimer = 0f;
            cameraFollow.cameraTarget = cameraPosition;
        }
    }

    private void CameraHoldTimer()
    {
        if (holdPositionTime < holdTimer) {
            cameraFollow.cameraTarget = null;
        }

        holdTimer += Time.deltaTime;
    }
}
