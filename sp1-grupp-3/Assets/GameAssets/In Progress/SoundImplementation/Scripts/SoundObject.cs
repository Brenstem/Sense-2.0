using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    [SerializeField] bool onEnable;
    [SerializeField] bool loop;
    [SerializeField] bool oneShot;

    [FMODUnity.EventRef]
    public string path;
    FMOD.Studio.EventInstance soundObject;

    private void OnEnable()
    {
        if (onEnable == true && oneShot == true)
        {
            PlayOneShotObject();
        }

        if (onEnable == true && loop == true)
        {
            PlayLoopObject();
        }
    }

    public void PlayLoopObject()
    {
        soundObject = FMODUnity.RuntimeManager.CreateInstance(path);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundObject, this.transform, this.GetComponent<Rigidbody>());
        //soundObject.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));

        soundObject.start();
        soundObject.release();
    }

    public void PlayOneShotObject()
    {
        soundObject = FMODUnity.RuntimeManager.CreateInstance(path);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundObject, this.transform, this.GetComponent<Rigidbody>());

        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);

        ////soundObject.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        //soundObject.start();
        //soundObject.release();
        //soundObject.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ////FMODUnity.RuntimeManager.DetachInstanceFromGameObject(soundObject);
    }

    public void OnDisable()
    {
        soundObject.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        FMODUnity.RuntimeManager.DetachInstanceFromGameObject(soundObject);
    }
}