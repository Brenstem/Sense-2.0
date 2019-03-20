using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private float menuTimer;
    private float onTransition;
    private float eq;
    private bool gameStart;

    [FMODUnity.EventRef]
    public string menuMusicSound;
    FMOD.Studio.EventInstance menuMusic;

    void Start()
    {
        PlaySound(sounds.menuMusic);
        menuTimer = 0f;
        onTransition = 0f;
        eq = 0f;
        gameStart = false;
    }

    public void PlaySound(sounds sound)
    {
        menuMusic = FMODUnity.RuntimeManager.CreateInstance(menuMusicSound);
        menuMusic.start();
    }

    public void Update()
    {
        MenuTransition();
    }

    private void MenuTransition()
    {
        menuTimer += Time.deltaTime;

        if (menuTimer > 298.0f)
        {
            onTransition = 1f;
            menuMusic.setParameterValue("Menu Timer", onTransition);
        }
    }

    public void StartEQ()
    {
        eq = 10f;
        menuMusic.setParameterValue("StartEQ", eq);
        StopMusic();
    }

    IEnumerator StopMusic()
    {
        yield return new WaitForSeconds(5f);
        menuMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        menuMusic.release();
    }
}
