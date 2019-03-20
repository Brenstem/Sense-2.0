using PathCreation;
using System.Collections;
using UnityEngine;
using FMODUnity;

public class Platform : MonoBehaviour
{
    public PathCreator pathCreator;
    public bool move;
    public float speed = 5;
    public EndOfPathInstruction platformBehaviour;
    public float waitTime;
    [HideInInspector] public Vector2 velocity;

    private StudioEventEmitter soundEvent;

    private float distanceTravelled;
    private Vector2 lastPosition;


    private void Start()
    {
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        soundEvent = GetComponent<StudioEventEmitter>();
    }

    private void Update()
    {
        if (velocity.x != 0 || velocity.y != 0)
        {
            if (!soundEvent.IsPlaying())
            {
                soundEvent.Play();
            }
        }
        else
        {
            soundEvent.Stop();
        }
    }

    void FixedUpdate()
    {
        velocity = GetKinematicVelocity();
        MoveAlongPath();        
    }

    private void MoveAlongPath()
    {
        if (move) {
            distanceTravelled += speed * Time.deltaTime;
        }
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, platformBehaviour);

        if (platformBehaviour == EndOfPathInstruction.Reverse && waitTime > 0.00f) {
            WaitReverse();
        }
        ClampPercentage0To2();

        //Makes 2D sprites face camera
        //Quaternion rotationOffsetY = Quaternion.Euler(Vector3.up * 90);
        //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled) * rotationOffsetY;
    }

    private void ClampPercentage0To2()
    {
        if (CalculatePercentage() >= 2) {
            distanceTravelled = 0.0f;
        }
    }

    private void WaitReverse()
    {
        float distancePercentTotal = CalculatePercentage();
        float distancePerStep = Time.deltaTime / speed;
        float tolerance = 1 - distancePerStep; //Half the movement distance per step on each side of 1


        if (distancePercentTotal < 1 && (1 - distancePerStep) < distancePercentTotal && move) { // 1 means 100% of the path length
            distanceTravelled = pathCreator.path.length;
            StartCoroutine(Wait(waitTime));
        }
        else if (distancePercentTotal > (2 - distancePerStep) && move) { // 2 means 200% of the path length, (the whole path length forward and back)
            distanceTravelled = pathCreator.path.length * 2;
            StartCoroutine(Wait(waitTime));
        }

        
    }

    private IEnumerator Wait(float waitTime)
    {
        move = false;
        yield return new WaitForSeconds(waitTime);
        move = true;
        distanceTravelled += speed * Time.deltaTime;
    }

    private Vector2 GetKinematicVelocity()
    {
        Vector2 currentPosition = transform.position;
        Vector2 velocityReturn = (currentPosition - lastPosition) / Time.fixedDeltaTime; // Divided by fixedDeltaTime to scale with velocity
        lastPosition = currentPosition;

        return velocityReturn;
    }

    private float CalculatePercentage()
    {
        return distanceTravelled / pathCreator.path.length;
    }
}
