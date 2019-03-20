using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;

public class LeverObject : MonoBehaviour
{
    #region Fields

    public enum ObjectAction { DoNothing, MoveToPosition, MoveByDistance, ActivatePhysics, PlaySound, StopSound }

    [SerializeField] public ObjectAction objectAction;
    public Vector2 moveTo;
    public Vector2 moveBy;
    public float moveSpeed = 1;
    public UnityEvent invokedMethod;
    
    private StudioEventEmitter soundEvent;
    private Vector2 currentPosition;
    private float moveIncrement;

    private bool actionPerformed = false;

    #endregion

    #region Set/Get Functions

    public bool ActionPerformed { get => actionPerformed; private set => actionPerformed = value; }

    #endregion

    #region Internal Methods

    private void Awake()
    {
        soundEvent = GetComponent<StudioEventEmitter>();
    }

    private void Update()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        if (actionPerformed && objectAction == ObjectAction.MoveToPosition) {
            transform.position = Vector2.MoveTowards(currentPosition, moveTo, moveIncrement);

            moveIncrement += Time.deltaTime * moveSpeed;
        }

        else if (actionPerformed && objectAction == ObjectAction.MoveByDistance) {
            Vector2 localMovement = moveBy + currentPosition;

            transform.position = Vector2.MoveTowards(currentPosition, localMovement, moveIncrement);

            moveIncrement += Time.deltaTime * moveSpeed;
        }
    }

    private void ActivatePhysics()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null) {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void PlaySound()
    {
        soundEvent.Play();
    }

    private void StopSound()
    {
        soundEvent.Stop();
    }

    #endregion

    #region Public Methods

    public void OnActivatedByLever()
    {
        Platform platform = GetComponent<Platform>();

        actionPerformed = true;

        if (platform == null) {
            switch (objectAction) {
                case ObjectAction.MoveToPosition:
                    currentPosition = transform.position;
                    moveIncrement = 0.0f;
                    break;

                case ObjectAction.MoveByDistance:
                    currentPosition = transform.position;
                    break;

                case ObjectAction.ActivatePhysics:
                    ActivatePhysics();
                    break;
                case ObjectAction.PlaySound:
                    PlaySound();
                    break;
                case ObjectAction.StopSound:
                    StopSound();
                    break;
                default:
                    break;
            }
        }
        else {
            platform.move = true;
        }
    }

    #endregion

}
