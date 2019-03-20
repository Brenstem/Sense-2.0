using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] bool scrolling, parallax;
    [SerializeField] float backgroundImageSize;
    [SerializeField] float yOffset;
    [SerializeField] float parallaxSpeed;
    [SerializeField] int zPosition;

    private Transform cameraPosition;
    private Transform[] images;
    private float viewZone = 0;
    private float lastCameraX;
    private int leftIndex;
    private int rightIndex;

    private void Start()
    {
        cameraPosition = Camera.main.transform;
        lastCameraX = cameraPosition.position.x;
        images = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            images[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = images.Length - 1;
    }

    private void Update()
    {
        if (parallax)
        {
            float deltaX = cameraPosition.position.x - lastCameraX;
            transform.position += new Vector3(1 * deltaX * parallaxSpeed, 0);
        }
        

        lastCameraX = cameraPosition.position.x;

        if (scrolling)
        {
            if (cameraPosition.position.x < images[leftIndex].transform.position.x + viewZone)
                ScrollLeft();

            if (cameraPosition.position.x > images[rightIndex].transform.position.x - viewZone)
                ScrollRight();
        }
        
    }

    private void ScrollLeft()
    {
        images[rightIndex].position = new Vector3(1 * (images[leftIndex].position.x - backgroundImageSize), yOffset, zPosition);

        leftIndex = rightIndex;
        rightIndex--;

        if (rightIndex < 0)
        {
            rightIndex = images.Length - 1;
        }
    }

    private void ScrollRight()
    {
        images[leftIndex].position = new Vector3(1 * (images[rightIndex].position.x + backgroundImageSize), yOffset, zPosition);

        rightIndex = leftIndex;
        leftIndex++;

        if (leftIndex == images.Length)
        {
            leftIndex = 0;
        }
    }
}
