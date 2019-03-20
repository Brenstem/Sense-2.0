﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicJunkyard_transition : MonoBehaviour
{
    public MusicJunkyard music;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            music.Transition1();
        }
    }
}