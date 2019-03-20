using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicJunkyatd_transition3 : MonoBehaviour
{
    public MusicJunkyard music;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            music.Transition3();
        }
    }
}
