using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbTrigger : MonoBehaviour
{
    public bool attachToObject;

    [FMODUnity.EventRef]
    public string path;
    FMOD.Studio.EventInstance reverbObject;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            reverbObject = FMODUnity.RuntimeManager.CreateInstance(path);
            if (attachToObject)
            {
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(reverbObject, this.transform, this.GetComponent<Rigidbody>());
            }
            reverbObject.start();
            reverbObject.release();
        }
    }
    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (attachToObject)
            {
                FMODUnity.RuntimeManager.DetachInstanceFromGameObject(reverbObject);
            }
            else
            {
                reverbObject.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }        
    }
}
