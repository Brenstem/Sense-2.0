using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BoxSound : MonoBehaviour
{
    GroundCheckAdvanced gAdvanced;
    public StudioEventEmitter landSound, dragSound;
    bool hasPlayedDraggedSound = false;
    bool previousRight = false, previousLeft = false, previousUp = false, previousDown = false;

    void Start()
    {
        gAdvanced = GetComponent<GroundCheckAdvanced>();
    }

    void Update()
    {
        if(gAdvanced.HasLanded(Vector2.down, ref previousDown))
        {
            landSound.Play();
        }
        if(gAdvanced.HasLanded(Vector2.right, ref previousRight))
        {
            landSound.Play();
        }
        if(gAdvanced.HasLanded(Vector2.left, ref previousLeft))
        {
            landSound.Play();
        }
        if(gAdvanced.HasLanded(Vector2.up, ref previousUp))
        {
            landSound.Play();
        }

        if (gAdvanced.IsDraggedOnGround() == true)
        {
            if (hasPlayedDraggedSound == false)
            {
                dragSound.Play();
                hasPlayedDraggedSound = true;
            }
        }
        else
        {
            dragSound.Stop();
            hasPlayedDraggedSound = false;
        }
    }
}