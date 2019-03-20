using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicJunkyard : MonoBehaviour
{
    private float onTransition1;
    private float onTransition2;
    private float onTransition3;
    private float toPause;
    private bool hasPlayed;

    [FMODUnity.EventRef]
    public string path;
    FMOD.Studio.EventInstance sound;

    private void Start()
    {
        onTransition1 = 0f;
        onTransition2 = 0f;
        onTransition3 = 0f;
        toPause = 0f;
        hasPlayed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && hasPlayed == false)
        {
            sound = FMODUnity.RuntimeManager.CreateInstance(path);
            sound.start();
        }
        if (collision.gameObject.CompareTag("Player") && hasPlayed == true)
        {
            sound.setPaused(false);
            toPause = 0f;
            sound.setParameterValue("To Pause", toPause);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Pause());
        }
    }

    IEnumerator Pause()
    {
        toPause = 10f;
        sound.setParameterValue("To Pause", toPause);
        hasPlayed = true;
        yield return new WaitForSeconds(5.5f);
        sound.setPaused(true);
    }

    private void Stop(FMOD.Studio.EventInstance sound)
    {
        sound.release();
        sound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void Transition1()
    {
        onTransition1 = 1f;
        sound.setParameterValue("Loop1 End", onTransition1);

    }
    public void Transition2()
    {
        onTransition2 = 1f;
        sound.setParameterValue("Loop2 End", onTransition2);

    }
    public void Transition3()
    {
        onTransition3 = 1f;
        sound.setParameterValue("Loop3 End", onTransition3);

    }

}