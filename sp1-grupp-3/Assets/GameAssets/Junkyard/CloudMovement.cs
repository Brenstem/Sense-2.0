using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    [SerializeField] float speed;

    void Update()
    {
        transform.position += new Vector3(speed, 0);    
    }
}
