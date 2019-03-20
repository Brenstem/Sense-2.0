using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientPlayer : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string path;
    FMOD.Studio.EventInstance sound;

    public bool playOnce;
    private bool hasPlayed;

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {
            sound = FMODUnity.RuntimeManager.CreateInstance(path);
            if (!hasPlayed)
            {
                sound.start();
                if (playOnce == true)
                {
                    hasPlayed = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {
            Stop(sound);
        }
    }

    private void Stop(FMOD.Studio.EventInstance sound)
    {
        sound.release();
        sound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        hasPlayed = false;
    }
}
