using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoCutscene : MonoBehaviour
{
    public bool isPlaying = false;
    public bool hasPlayed = false;
    [SerializeField] bool stopPlayer;
    [SerializeField] bool quitApplication;
    public GameObject player;

    private VideoPlayer videoPlayer;
    private PlayerMovement playerMovement;
    private Animator playerAnimator;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerAnimator = player.GetComponent<Animator>();
    }

    void Update()
    {
        if (videoPlayer.isPlaying)
        {
            if (stopPlayer)
            {
                FMODUnity.RuntimeManager.MuteAllEvents(true);
            }
        }

        if (isPlaying && !videoPlayer.isPlaying) {
            videoPlayer.Play();
            if (stopPlayer)
            {
                playerMovement.enabled = false;
            }
        }

        else if (!isPlaying) {
            videoPlayer.Stop();
            FMODUnity.RuntimeManager.MuteAllEvents(false);
        }

        videoPlayer.loopPointReached += StopVideo;        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Play();
    }

    public void Play()
    {
        if (!hasPlayed)
        {
            if (stopPlayer)
            {
                videoPlayer.targetCamera.GetComponent<ColorChangerEffect>().ActivateEffect();
            }

            isPlaying = true;
            hasPlayed = true;
        }
    }

    private void StopVideo(VideoPlayer vp)
    {
        vp.Stop();
        isPlaying = false;

        if (stopPlayer)
        {
            playerMovement.enabled = true; 
        }

        if (quitApplication)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}
