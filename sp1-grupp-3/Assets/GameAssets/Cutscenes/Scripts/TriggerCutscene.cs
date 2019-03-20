using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerCutscene : MonoBehaviour
{
    // Serialized variables
    [SerializeField] GameObject[] images;

    // Private variables
    private Animator[] imageAnimators = new Animator[2];
    private Timer animationTimer;
    private bool animationPlaying;
    private bool cutSceneActivated;
    public int imageIndex = 0;

    // Gets animator components from images
    private void Start()
    {
        imageAnimators = new Animator[images.Length];
        animationTimer = new Timer();
        
        for (int i = 0; i < images.Length; i++)
        {
            imageAnimators[i] = images[i].GetComponent<Animator>();
        }
    }

    // Updates timer and cutscene playing
    private void Update()
    {
        animationTimer.UpdateTimer();

        if (cutSceneActivated)
        {
            PlayCutscene();
        }
    }
    
    // Activates cutscene playing on trigger enter
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag("Player"))
        {
            cutSceneActivated = true;
            this.GetComponent<Collider2D>().enabled = false;
        }
    }

    // Sets cutscene images to active one after another
    private void PlayCutscene()
    {
        for (; imageIndex < images.Length;)
        {
            if (!animationPlaying)
            {
                images[imageIndex].SetActive(true);
                animationTimer.StartTimer(imageAnimators[imageIndex].GetCurrentAnimatorStateInfo(0).length);
                animationPlaying = true;
            }
            break;
        }

        if (animationTimer.TimerFinished || Input.anyKeyDown)
        {
            if (images[imageIndex] != null) {
                images[imageIndex].SetActive(false);
               
            }
            if(imageIndex == images.Length - 1) {
                cutSceneActivated = false;
                imageIndex = 0;
                return;
            }
            animationPlaying = false;
            imageIndex++;
        }
    }
}
