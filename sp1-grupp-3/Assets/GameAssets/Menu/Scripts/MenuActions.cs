using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    // Serialized variables
    [SerializeField] GameObject creditsHolder;
    [SerializeField] GameObject Title;
    [SerializeField] GameObject buttonHolder;
    [SerializeField] GameObject levelChanger;
    [SerializeField] GameObject cutsceneTrigger;

    // Private variables
    private Animator creditsAnim;
    private Timer creditsTimer;
    private MenuMusic menuMusic;

    // Sound references
    [FMODUnity.EventRef]
    public string onSelectSound;
    FMOD.Studio.EventInstance onSelect;

    [FMODUnity.EventRef]
    public string onStartSound;
    FMOD.Studio.EventInstance onStart;

    [FMODUnity.EventRef]
    public string onExitSound;
    FMOD.Studio.EventInstance onExit;


    // Reference fetching
    private void Start()
    {
        creditsAnim = creditsHolder.GetComponent<Animator>();
        creditsTimer = new Timer();
        menuMusic = GetComponent<MenuMusic>();

#if UNITY_EDITOR
#else
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
#endif
    }


    private void Update()
    {
        creditsTimer.UpdateTimer();

        if (creditsTimer.TimerFinished)
        {
            creditsHolder.SetActive(false);
            buttonHolder.SetActive(true);
            Title.SetActive(true);
        }
    }

    // Quits game/stops playmode depending on build
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Scene loading

    public void LoadScene(int sceneIndex)
    {
        levelChanger.GetComponent<LevelChanger>().FadeToLevel(sceneIndex);
    }

    //Sound playing
    public void PlayStartSound()
    {
        menuMusic.StartEQ();

        onStart = FMODUnity.RuntimeManager.CreateInstance(onStartSound);
        onStart.start();
        onStart.release();
    }

    public void PlaySelectSound()
    {
        onSelect = FMODUnity.RuntimeManager.CreateInstance(onSelectSound);
        onSelect.start();
        onSelect.release();
    }

    public void PlayExitSound()
    {
        onExit = FMODUnity.RuntimeManager.CreateInstance(onExitSound);
        onExit.start();
        onExit.release();
    }

    // Credits playing
    public void PlayCredits()
    {
        //creditsHolder.SetActive(true);
        buttonHolder.SetActive(false);
        Title.SetActive(false);
        cutsceneTrigger.GetComponent<VideoCutscene>().Play();
        creditsTimer.StartTimer(45);
    }
}
