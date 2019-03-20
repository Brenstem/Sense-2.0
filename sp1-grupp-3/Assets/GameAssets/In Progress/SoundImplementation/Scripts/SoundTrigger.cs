using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public bool playOnce;
    public bool stopOnExit;
    public bool attachToObject;
    private bool hasPlayed;
    public string setTag;

    [FMODUnity.EventRef]
    public string path;
    FMOD.Studio.EventInstance soundObject;

    private void Start()
    {
        hasPlayed = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!playOnce)
        {
            if (collider.CompareTag("Player"))
            {
                PlaySound();
            }
        }
        if (playOnce)
        {
            if (hasPlayed == false && collider.CompareTag("Player"))
            {
                PlaySound();
                hasPlayed = true;
                return;
            }
        }
    }

    public void PlaySound()
    {
        soundObject = FMODUnity.RuntimeManager.CreateInstance(path);
        if (attachToObject) // Kräver AddEffects>"Spatializer" i FMOD
        {
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundObject, this.transform, this.GetComponent<Rigidbody>());
        }
        soundObject.start();
        soundObject.release();
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag(setTag))
        {
            if (stopOnExit == true)
            {
                soundObject.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }
    }

    public void OnDestroy()
    {
        FMODUnity.RuntimeManager.DetachInstanceFromGameObject(soundObject);
        soundObject.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
