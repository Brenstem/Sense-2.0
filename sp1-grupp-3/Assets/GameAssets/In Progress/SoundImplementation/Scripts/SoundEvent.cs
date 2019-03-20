using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvent : MonoBehaviour
{
    //Loop instance
    [HideInInspector] public FMOD.Studio.EventInstance loopSound;

    public void PlayOneShot(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }

    public void PlayLoopable(string path)
    {
        loopSound = FMODUnity.RuntimeManager.CreateInstance(path);
        loopSound.start();
    }

    public void OnDestroyLoop(string path)
    {
        loopSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        loopSound.release();
    }
}